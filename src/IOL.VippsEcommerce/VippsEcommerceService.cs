using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using IOL.VippsEcommerce.Models;
using IOL.VippsEcommerce.Models.Api;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IOL.VippsEcommerce
{
	public class VippsEcommerceService : IVippsEcommerceService

	{
		private readonly HttpClient _client;
		private readonly ILogger<VippsEcommerceService> _logger;
		private readonly string _vippsClientId;
		private readonly string _vippsClientSecret;
		private readonly string _vippsMsn;
		private readonly string _cacheEncryptionKey;
		private readonly string _cacheFileDirectoryPath;

		private const string VIPPS_CACHE_FILE_NAME = "vipps_ecommerce_credentials.json";
		private string CacheFilePath => Path.Combine(_cacheFileDirectoryPath, VIPPS_CACHE_FILE_NAME);

		public VippsEcommerceService(
			HttpClient client,
			ILogger<VippsEcommerceService> logger,
			IOptions<VippsConfiguration> options
		) {
			var configuration = options.Value;
			var vippsApiUrl = configuration.GetValue(VippsConfigurationKeyNames.VIPPS_API_URL);
			if (vippsApiUrl.IsNullOrWhiteSpace()) {
				throw new ArgumentException("VippsEcommerceService: Api url is not provided in configuration.");
			}

			client.BaseAddress = new Uri(vippsApiUrl);
			_client = client;
			_logger = logger;

			_vippsClientId = configuration.GetValue(VippsConfigurationKeyNames.VIPPS_CLIENT_ID);
			if (_vippsClientId.IsNullOrWhiteSpace()) {
				throw new ArgumentException("VippsEcommerceService: Client id is not provided in configuration.");
			}

			_vippsClientSecret = configuration.GetValue(VippsConfigurationKeyNames.VIPPS_CLIENT_SECRET);
			if (_vippsClientSecret.IsNullOrWhiteSpace()) {
				throw new ArgumentException("VippsEcommerceService: Client secret is not provided in configuration.");
			}

			var primarySubscriptionKey =
				configuration.GetValue(VippsConfigurationKeyNames.VIPPS_SUBSCRIPTION_KEY_PRIMARY);
			var secondarySubscriptionKey =
				configuration.GetValue(VippsConfigurationKeyNames.VIPPS_SUBSCRIPTION_KEY_SECONDARY);
			if (primarySubscriptionKey.IsNullOrWhiteSpace() && secondarySubscriptionKey.IsNullOrWhiteSpace()) {
				throw new
					ArgumentException("VippsEcommerceService: Neither primary nor secondary subscription key was provided in configuration.");
			}

			client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key",
			                                 primarySubscriptionKey ?? secondarySubscriptionKey);

			var msn = configuration.GetValue(VippsConfigurationKeyNames.VIPPS_MSN);
			if (msn.IsPresent()) {
				client.DefaultRequestHeaders.Add("Merchant-Serial-Number", msn);
				_vippsMsn = msn;
			}

			var systemName = configuration.GetValue(VippsConfigurationKeyNames.VIPPS_SYSTEM_NAME);
			if (systemName.IsPresent()) {
				client.DefaultRequestHeaders.Add("Vipps-System-Name", systemName);
			}

			var systemVersion = configuration.GetValue(VippsConfigurationKeyNames.VIPPS_SYSTEM_VERSION);
			if (systemVersion.IsPresent()) {
				client.DefaultRequestHeaders.Add("Vipps-System-Version", systemVersion);
			}

			var systemPluginName = configuration.GetValue(VippsConfigurationKeyNames.VIPPS_SYSTEM_PLUGIN_NAME);
			if (systemPluginName.IsPresent()) {
				client.DefaultRequestHeaders.Add("Vipps-System-Plugin-Name", systemPluginName);
			}

			var systemPluginVersion = configuration.GetValue(VippsConfigurationKeyNames.VIPPS_SYSTEM_PLUGIN_VERSION);
			if (systemPluginVersion.IsPresent()) {
				client.DefaultRequestHeaders.Add("Vipps-System-Plugin-Version", systemPluginVersion);
			}

			_cacheEncryptionKey = configuration.GetValue(VippsConfigurationKeyNames.VIPPS_CACHE_KEY);
			_cacheFileDirectoryPath = configuration.GetValue(VippsConfigurationKeyNames.VIPPS_CACHE_PATH);
			if (_cacheFileDirectoryPath.IsPresent()) {
				if (!_cacheFileDirectoryPath.IsDirectoryWritable()) {
					_logger.LogError("Could not write to cache file directory ("
					                 + _cacheFileDirectoryPath
					                 + "). Disabling caching.");
					_cacheFileDirectoryPath = default;
					_cacheEncryptionKey = default;
				}
			}

			_logger.LogInformation("VippsEcommerceService was initialized with api url: " + vippsApiUrl);
		}

		/// <summary>
		/// The access token endpoint is used to get the JWT (JSON Web Token) that must be passed in every API request in the Authorization header.
		/// The access token is a base64-encoded string value that must be aquired first before making any Vipps api calls.
		/// The access token is valid for 1 hour in the test environment and 24 hours in the production environment.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="HttpRequestException">Throws if the api returns unsuccessfully</exception>
		private async Task<VippsAuthorizationTokenResponse> GetAuthorizationTokenAsync(
			bool forceRefresh = false,
			CancellationToken ct = default
		) {
			if (!forceRefresh) {
				if (_cacheFileDirectoryPath.IsPresent() && File.Exists(CacheFilePath)) {
					var fileContents = await File.ReadAllTextAsync(CacheFilePath, ct);

					if (fileContents.IsPresent()) {
						VippsAuthorizationTokenResponse credentials = default;
						try {
							credentials = JsonSerializer.Deserialize<VippsAuthorizationTokenResponse>(fileContents);
						} catch (Exception e) {
							if (e is JsonException && _cacheEncryptionKey.IsPresent()) {
								// most likely encrypted, try to decrypt
								var decryptedContents = fileContents.DecryptWithAes(_cacheEncryptionKey);
								credentials =
									JsonSerializer.Deserialize<VippsAuthorizationTokenResponse>(decryptedContents);
							}
						}

						if (credentials != default) {
							var currentEpoch = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
							if (long.TryParse(credentials.ExpiresOn, out var expires)
							    && credentials.AccessToken.IsPresent()) {
								if (expires - 600 > currentEpoch) {
									_logger.LogDebug("VippsEcommerceService: Got tokens from cache");
									return credentials;
								}
							}
						}
					}
				}
			}

			var requestMessage = new HttpRequestMessage {
				Headers = {
					{
						"client_id", _vippsClientId
					}, {
						"client_secret", _vippsClientSecret
					},
				},
				RequestUri = new Uri(_client.BaseAddress + "accesstoken/get"),
				Method = HttpMethod.Post
			};
			var response = await _client.SendAsync(requestMessage, ct);

			try {
				response.EnsureSuccessStatusCode();
				var credentials = await response.Content.ReadAsStringAsync(ct);

				if (_cacheFileDirectoryPath.IsPresent()) {
					await File.WriteAllTextAsync(CacheFilePath,
					                             _cacheEncryptionKey.IsPresent()
						                             ? credentials.EncryptWithAes(_cacheEncryptionKey)
						                             : credentials,
					                             ct);
				}

				_logger.LogDebug("VippsEcommerceService: Got tokens from " + requestMessage.RequestUri);
				return JsonSerializer.Deserialize<VippsAuthorizationTokenResponse>(credentials);
			} catch (Exception e) {
				Console.WriteLine(e);
				return default;
			}
		}


		/// <summary>
		/// This API call allows the merchants to initiate payments.
		/// The merchantSerialNumber (MSN) specifies which sales unit the payments is for.
		/// Payments are uniquely identified with the merchantSerialNumber and orderId together.
		/// The merchant-provided orderId must be unique per sales channel.
		/// Once the transaction is successfully initiated in Vipps, you will receive a response with a fallBack URL which will direct the customer to the Vipps landing page.
		/// The landing page detects if the request comes from a mobile or laptop/desktop device, and if on a mobile device automatically switches to the Vipps app if it is intalled.
		/// The merchant may also pass the 'isApp: true' parameter that will make Vipps respond with a app-switch deeplink that will take the customer directly to the Vipps app.
		/// URLs passed to Vipps must validate with the Apache Commons UrlValidator.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="HttpRequestException">Throws if the api returns unsuccessfully</exception>
		public async Task<VippsInitiatePaymentResponse> InitiatePaymentAsync(
			VippsInitiatePaymentRequest payload,
			CancellationToken ct = default
		) {
			if (_client.DefaultRequestHeaders.Authorization?.Parameter.IsNullOrWhiteSpace() ?? true) {
				var credentials = await GetAuthorizationTokenAsync(false, ct);
				_client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", credentials.AccessToken);
			}

			var response = await _client.PostAsJsonAsync("ecomm/v2/payments",
			                                             payload,
			                                             new JsonSerializerOptions {
				                                             IgnoreNullValues = true
			                                             },
			                                             ct);

			try {
				response.EnsureSuccessStatusCode();
				_logger.LogDebug("VippsEcommerceService: Sent InitiatePaymentRequest");
				return await response.Content
				                     .ReadFromJsonAsync<VippsInitiatePaymentResponse>(cancellationToken: ct);
			} catch (Exception e) {
				var exception =
					new VippsRequestException("Vipps initiate payment request returned unsuccessfully.", e);
				if (e is HttpRequestException) {
					try {
						exception.ErrorResponse =
							await response.Content.ReadFromJsonAsync<VippsErrorResponse>(cancellationToken: ct);
						_logger.LogError("ErrorResponse: " + JsonSerializer.Serialize(response.Content));
					} catch (Exception e1) {
						_logger.LogError("Unknown ErrorResponse: " + JsonSerializer.Serialize(response.Content));
						Console.WriteLine(e1);
					}
				}

				Console.WriteLine(e);
				throw exception;
			}
		}

		/// <summary>
		/// This API call allows merchant to capture the reserved amount.
		/// Amount to capture cannot be higher than reserved.
		/// The API also allows capturing partial amount of the reserved amount.
		/// Partial capture can be called as many times as required so long there is reserved amount to capture.
		/// Transaction text is not optional and is used as a proof of delivery (tracking code, consignment number etc.).
		/// In a case of direct capture, both fund reservation and capture are executed in a single operation.
		/// It is important to check the response, and the capture is only successful when the response is HTTP 200 OK.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="HttpRequestException">Throws if the api returns unsuccessfully</exception>
		public async Task<VippsPaymentActionResponse> CapturePaymentAsync(
			string orderId,
			VippsPaymentActionRequest payload,
			CancellationToken ct = default
		) {
			if (_client.DefaultRequestHeaders.Authorization?.Parameter.IsNullOrWhiteSpace() ?? true) {
				var credentials = await GetAuthorizationTokenAsync(false, ct);
				_client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", credentials.AccessToken);
			}


			if (payload.MerchantInfo?.MerchantSerialNumber.IsNullOrWhiteSpace() ?? false) {
				payload.MerchantInfo = new TMerchantInfoPayment {
					MerchantSerialNumber = _vippsMsn
				};
			}

			var response = await _client.PostAsJsonAsync("ecomm/v2/payments/" + orderId + "/capture",
			                                             payload,
			                                             new JsonSerializerOptions {
				                                             IgnoreNullValues = true
			                                             },
			                                             ct);

			try {
				response.EnsureSuccessStatusCode();
				_logger.LogDebug("VippsEcommerceService: Sent CapturePaymentRequest");
				return await response.Content.ReadFromJsonAsync<VippsPaymentActionResponse>(cancellationToken: ct);
			} catch (Exception e) {
				var exception =
					new VippsRequestException("Vipps capture payment request returned unsuccessfully.", e);
				if (e is HttpRequestException) {
					try {
						exception.ErrorResponse =
							await response.Content.ReadFromJsonAsync<VippsErrorResponse>(cancellationToken: ct);
						_logger.LogError("ErrorResponse: " + JsonSerializer.Serialize(response.Content));
					} catch (Exception e1) {
						_logger.LogError("Unknown ErrorResponse: " + JsonSerializer.Serialize(response.Content));
						Console.WriteLine(e1);
					}
				}

				Console.WriteLine(e);
				throw exception;
			}
		}


		/// <summary>
		/// The API call allows merchant to cancel the reserved or initiated transaction.
		/// The API will not allow partial cancellation which has the consequence that partially captured transactions cannot be cancelled.
		/// Please note that in a case of communication errors during initiate payment service call between Vipps and PSP/Acquirer/Issuer; even in a case that customer has confirmed a payment, the payment will be cancelled by Vipps.
		/// Note this means you can not cancel a captured payment.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="HttpRequestException">Throws if the api returns unsuccessfully</exception>
		public async Task<VippsPaymentActionResponse> CancelPaymentAsync(
			string orderId,
			VippsPaymentActionRequest payload,
			CancellationToken ct = default
		) {
			if (_client.DefaultRequestHeaders.Authorization?.Parameter.IsNullOrWhiteSpace() ?? true) {
				var credentials = await GetAuthorizationTokenAsync(false, ct);
				_client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", credentials.AccessToken);
			}

			if (payload.MerchantInfo?.MerchantSerialNumber.IsNullOrWhiteSpace() ?? false) {
				payload.MerchantInfo = new TMerchantInfoPayment {
					MerchantSerialNumber = _vippsMsn
				};
			}

			var response = await _client.PutAsJsonAsync("ecomm/v2/payments/" + orderId + "/cancel",
			                                            payload,
			                                            new JsonSerializerOptions {
				                                            IgnoreNullValues = true
			                                            },
			                                            ct);

			try {
				response.EnsureSuccessStatusCode();
				_logger.LogDebug("VippsEcommerceService: Sent CancelPaymentRequest");
				return await response.Content.ReadFromJsonAsync<VippsPaymentActionResponse>(cancellationToken: ct);
			} catch (Exception e) {
				var exception =
					new VippsRequestException("Vipps cancel payment request returned unsuccessfully.", e);
				if (e is HttpRequestException) {
					try {
						exception.ErrorResponse =
							await response.Content.ReadFromJsonAsync<VippsErrorResponse>(cancellationToken: ct);
						_logger.LogError("ErrorResponse: " + JsonSerializer.Serialize(response.Content));
					} catch (Exception e1) {
						_logger.LogError("Unknown ErrorResponse: " + JsonSerializer.Serialize(response.Content));
						Console.WriteLine(e1);
					}
				}


				Console.WriteLine(e);
				throw exception;
			}
		}

		/// <summary>
		/// The API call allows merchant to refresh the authorizations of the payment.
		/// A reservation's lifetime is defined by the scheme. Typically 7 days for Visa, and 30 days for Mastercard.
		/// This is currently not live in production and will be added shortly.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="HttpRequestException">Throws if the api returns unsuccessfully</exception>
		public async Task<VippsPaymentActionResponse> AuthorizePaymentAsync(
			string orderId,
			VippsPaymentActionRequest payload,
			CancellationToken ct = default
		) {
			if (_client.DefaultRequestHeaders.Authorization?.Parameter.IsNullOrWhiteSpace() ?? true) {
				var credentials = await GetAuthorizationTokenAsync(false, ct);
				_client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", credentials.AccessToken);
			}

			if (payload.MerchantInfo?.MerchantSerialNumber.IsNullOrWhiteSpace() ?? false) {
				payload.MerchantInfo = new TMerchantInfoPayment {
					MerchantSerialNumber = _vippsMsn
				};
			}

			var response = await _client.PutAsJsonAsync("ecomm/v2/payments/" + orderId + "/authorize",
			                                            payload,
			                                            new JsonSerializerOptions {
				                                            IgnoreNullValues = true
			                                            },
			                                            ct);

			try {
				response.EnsureSuccessStatusCode();
				_logger.LogDebug("VippsEcommerceService: Sent AuthorizePaymentRequest");
				return await response.Content.ReadFromJsonAsync<VippsPaymentActionResponse>(cancellationToken: ct);
			} catch (Exception e) {
				var exception =
					new VippsRequestException("Vipps authorize payment request returned unsuccessfully.", e);
				if (e is HttpRequestException) {
					try {
						exception.ErrorResponse =
							await response.Content.ReadFromJsonAsync<VippsErrorResponse>(cancellationToken: ct);
						_logger.LogError("ErrorResponse: " + JsonSerializer.Serialize(response.Content));
					} catch (Exception e1) {
						_logger.LogError("Unknown ErrorResponse: " + JsonSerializer.Serialize(response.Content));
						Console.WriteLine(e1);
					}
				}


				Console.WriteLine(e);
				throw exception;
			}
		}

		/// <summary>
		/// The API allows a merchant to do a refund of already captured transaction.
		/// There is an option to do a partial refund of the captured amount.
		/// Refunded amount cannot be larger than captured.
		/// Timeframe for issuing a refund for a payment is 365 days from the date payment has been captured.
		/// If the refund payment service call is called after the refund timeframe, service call will respond with an error.
		/// Refunded funds will be transferred from the merchant account to the customer credit card that was used in payment flow.
		/// Pay attention that in order to perform refund, there must be enough funds at merchant settlements account.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="HttpRequestException">Throws if the api returns unsuccessfully</exception>
		public async Task<VippsPaymentActionResponse> RefundPaymentAsync(
			string orderId,
			VippsPaymentActionRequest payload,
			CancellationToken ct = default
		) {
			if (_client.DefaultRequestHeaders.Authorization?.Parameter.IsNullOrWhiteSpace() ?? true) {
				var credentials = await GetAuthorizationTokenAsync(false, ct);
				_client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", credentials.AccessToken);
			}

			if (payload.MerchantInfo?.MerchantSerialNumber.IsNullOrWhiteSpace() ?? false) {
				payload.MerchantInfo = new TMerchantInfoPayment {
					MerchantSerialNumber = _vippsMsn
				};
			}

			var response = await _client.PostAsJsonAsync("ecomm/v2/payments/" + orderId + "/refund",
			                                             payload,
			                                             new JsonSerializerOptions {
				                                             IgnoreNullValues = true
			                                             },
			                                             ct);
			try {
				response.EnsureSuccessStatusCode();
				_logger.LogDebug("VippsEcommerceService: Sent RefundPaymentRequest");
				return await response.Content.ReadFromJsonAsync<VippsPaymentActionResponse>(cancellationToken: ct);
			} catch (Exception e) {
				var exception =
					new VippsRequestException("Vipps refund payment request returned unsuccessfully.", e);
				if (e is HttpRequestException) {
					try {
						exception.ErrorResponse =
							await response.Content.ReadFromJsonAsync<VippsErrorResponse>(cancellationToken: ct);
						_logger.LogError("ErrorResponse: " + JsonSerializer.Serialize(response.Content));
					} catch (Exception e1) {
						_logger.LogError("Unknown ErrorResponse: " + JsonSerializer.Serialize(response.Content));
						Console.WriteLine(e1);
					}
				}


				Console.WriteLine(e);
				throw exception;
			}
		}


		/// <summary>
		/// This endpoint allows developers to approve a payment through the Vipps eCom API without the use of the Vipps app.
		/// This is useful for automated testing.
		/// Express checkout is not supported for this endpoint.
		/// The endpoint is only available in our Test environment.
		/// Attempted use of the endpoint in production is not allowed, and will fail.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="HttpRequestException">Throws if the api returns unsuccessfully</exception>
		public async Task<bool> ForceApprovePaymentAsync(
			string orderId,
			VippsForceApproveRequest payload,
			CancellationToken ct = default
		) {
			if (_client.DefaultRequestHeaders.Authorization?.Parameter.IsNullOrWhiteSpace() ?? true) {
				var credentials = await GetAuthorizationTokenAsync(false, ct);
				_client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", credentials.AccessToken);
			}


			var response =
				await _client.PostAsJsonAsync("ecomm/v2/integration-test/payments/" + orderId + "/approve",
				                              payload,
				                              new JsonSerializerOptions {
					                              IgnoreNullValues = true
				                              },
				                              ct);

			try {
				response.EnsureSuccessStatusCode();
				_logger.LogDebug("VippsEcommerceService: Sent ForceApprovePaymentRequest");
				return true;
			} catch (Exception e) {
				var exception =
					new VippsRequestException("Vipps force approve payment request returned unsuccessfully.", e);
				if (e is HttpRequestException) {
					try {
						exception.ErrorResponse =
							await response.Content.ReadFromJsonAsync<VippsErrorResponse>(cancellationToken: ct);
						_logger.LogError("ErrorResponse: " + JsonSerializer.Serialize(response.Content));
					} catch (Exception e1) {
						_logger.LogError("Unknown ErrorResponse: " + JsonSerializer.Serialize(response.Content));
						Console.WriteLine(e1);
					}
				}

				Console.WriteLine(e);
				throw exception;
			}
		}

		/// <summary>
		/// This API call allows merchant to get the details of a payment transaction.
		/// Service call returns detailed transaction history of given payment where events are sorted from newest to oldest for when the transaction occurred.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="HttpRequestException">Throws if the api returns unsuccessfully</exception>
		public async Task<VippsGetPaymentDetailsResponse> GetPaymentDetailsAsync(
			string orderId,
			CancellationToken ct = default
		) {
			if (_client.DefaultRequestHeaders.Authorization?.Parameter.IsNullOrWhiteSpace() ?? true) {
				var credentials = await GetAuthorizationTokenAsync(false, ct);
				_client.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", credentials.AccessToken);
			}

			var response = await _client.GetAsync("ecomm/v2/payments/" + orderId + "/details", ct);

			try {
				response.EnsureSuccessStatusCode();
				_logger.LogDebug("VippsEcommerceService: Sent GetPaymentDetailsRequest");
				return await
					response.Content.ReadFromJsonAsync<VippsGetPaymentDetailsResponse>(cancellationToken: ct);
			} catch (Exception e) {
				var exception =
					new VippsRequestException("Vipps get payment detailsG request returned unsuccessfully.", e);
				if (e is HttpRequestException) {
					try {
						exception.ErrorResponse =
							await response.Content.ReadFromJsonAsync<VippsErrorResponse>(cancellationToken: ct);
						_logger.LogError("ErrorResponse: " + JsonSerializer.Serialize(response.Content));
					} catch (Exception e1) {
						_logger.LogError("Unknown ErrorResponse: " + JsonSerializer.Serialize(response.Content));
						Console.WriteLine(e1);
					}
				}


				Console.WriteLine(e);
				throw exception;
			}
		}
	}
}