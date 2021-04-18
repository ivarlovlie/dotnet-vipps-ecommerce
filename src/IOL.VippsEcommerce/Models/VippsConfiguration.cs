using System;

namespace IOL.VippsEcommerce.Models
{
	/// <summary>
	/// Configuration fields for the vipps api and integration.
	/// </summary>
	public class VippsConfiguration
	{
		/// <summary>
		/// Url for the vipps api. This property is required.
		/// <example>https://apitest.vipps.no</example>
		/// <example>https://api.vipps.no</example>
		/// <para>Corresponding environment variable name: VIPPS_API_URL</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.API_URL)]
		public string ApiUrl { get; set; }

		/// <summary>
		/// Client ID for the merchant (the "username"). This property is required.
		/// <para>Corresponding environment variable name: VIPPS_CLIENT_ID</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.CLIENT_ID)]
		public string ClientId { get; set; }

		/// <summary>
		/// Client Secret for the merchant (the "password"). This property is required.
		/// <para>Corresponding environment variable name: VIPPS_CLIENT_SECRET</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.CLIENT_SECRET)]
		public string ClientSecret { get; set; }

		/// <summary>
		///	Primary subscription key for the API product.
		/// <para>The primary subscription key take precedence over the secondary subscription key.</para>
		/// <para>Either primary subscription key or secondary subscription key is required.</para>
		/// <para>Corresponding environment variable name: VIPPS_SUBSCRIPTION_KEY_PRIMARY</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.SUBSCRIPTION_KEY_PRIMARY)]
		public string PrimarySubscriptionKey { get; set; }

		/// <summary>
		///	Secondary subscription key for the API product.
		/// <para>The primary subscription key take precedence over the secondary subscription key.</para>
		/// <para>Either primary subscription key or secondary subscription key is required.</para>
		/// <para>Corresponding environment variable name: VIPPS_SUBSCRIPTION_KEY_SECONDARY</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.SUBSCRIPTION_KEY_SECONDARY)]
		public string SecondarySubscriptionKey { get; set; }

		/// <summary>
		/// The Merchant Serial Number (MSN) is a unique id for the sale unit that this payment is made for.
		/// <para>Corresponding environment variable name: VIPPS_MSN</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.MSN)]
		public string MerchantSerialNumber { get; set; }

		/// <summary>
		/// The name of the ecommerce solution. One word in lowercase letters is good.
		/// <para>Corresponding environment variable name: VIPPS_SYSTEM_NAME</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.SYSTEM_NAME)]
		public string SystemName { get; set; }

		/// <summary>
		/// The version number of the ecommerce solution.
		/// <para>Corresponding environment variable name: VIPPS_SYSTEM_VERSION</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.SYSTEM_VERSION)]
		public string SystemVersion { get; set; }

		/// <summary>
		/// The name of the ecommerce plugin (if applicable). One word in lowercase letters is good.
		/// <para>Corresponding environment variable name: VIPPS_SYSTEM_PLUGIN_NAME</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.SYSTEM_PLUGIN_NAME)]
		public string SystemPluginName { get; set; }

		/// <summary>
		/// The version number of the ecommerce plugin (if applicable).
		/// <para>Corresponding environment variable name: VIPPS_SYSTEM_PLUGIN_VERSION</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.SYSTEM_PLUGIN_VERSION)]
		public string SystemPluginVersion { get; set; }

		/// <summary>
		/// Optional path to a writable directory wherein a credential cache file can be placed.
		/// <para>Corresponding environment variable name: VIPPS_CACHE_PATH</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.CACHE_PATH)]
		public string CacheDirectoryPath { get; set; }

		/// <summary>
		/// Optional key for AES encryption of the credential cache file.
		/// <para>Corresponding environment variable name: VIPPS_CACHE_KEY</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.CACHE_KEY)]
		public string CacheEncryptionKey { get; set; }

		/// <summary>
		/// Specify how to retrieve configuration and/or in what order. Defaults to VippsConfigurationMode.ENVIRONMENT_THEN_OBJECT.
		/// </summary>
		public VippsConfigurationMode ConfigurationMode { get; set; } = VippsConfigurationMode.ENVIRONMENT_THEN_OBJECT;

		/// <summary>
		/// Get value from configuration, either from Dependency injection or from the environment.
		/// </summary>
		/// <param name="key">Configuration key.</param>
		/// <returns>A string containing the configuration value.</returns>
		public string GetValue(string key) {
			switch (ConfigurationMode) {
				case VippsConfigurationMode.ONLY_OBJECT:
					return GetValueFromObject(key);
				case VippsConfigurationMode.ONLY_ENVIRONMENT:
					return GetValueFromEnvironment(key);
				case VippsConfigurationMode.OBJECT_THEN_ENVIRONMENT:
					return GetValueFromObject(key) ?? GetValueFromEnvironment(key);
				case VippsConfigurationMode.ENVIRONMENT_THEN_OBJECT:
					return GetValueFromEnvironment(key) ?? GetValueFromObject(key);
				default:
					return default;
			}
		}

		/// <summary>
		/// Ensure that the configuration can be used to issue requests to the vipps api.
		/// <exception cref="ArgumentNullException">Throws if a required value is null or whitespace.</exception>
		/// </summary>
		public void Verify() {
			if (GetValue(VippsConfigurationKeyNames.API_URL).IsNullOrWhiteSpace()) {
				throw new ArgumentNullException(nameof(ApiUrl),
												"VippsEcommerceService: ApiUrl is not provided in configuration.");
			}

			if (GetValue(VippsConfigurationKeyNames.CLIENT_ID).IsNullOrWhiteSpace()) {
				throw new ArgumentNullException(nameof(ClientId),
												"VippsEcommerceService: ClientId is not provided in configuration.");
			}

			if (GetValue(VippsConfigurationKeyNames.CLIENT_SECRET).IsNullOrWhiteSpace()) {
				throw new ArgumentNullException(nameof(ClientSecret),
												"VippsEcommerceService: ClientSecret is not provided in configuration.");
			}

			if (GetValue(VippsConfigurationKeyNames.SUBSCRIPTION_KEY_PRIMARY).IsNullOrWhiteSpace()
				&& GetValue(VippsConfigurationKeyNames.SUBSCRIPTION_KEY_SECONDARY).IsNullOrWhiteSpace()) {
				throw new ArgumentNullException(nameof(PrimarySubscriptionKey)
												+ nameof(SecondarySubscriptionKey),
												"VippsEcommerceService: Neither PrimarySubscriptionKey nor SecondarySubscriptionKey was provided in configuration.");
			}
		}

		private string GetValueFromEnvironment(string key) {
#if DEBUG
			var value = Environment.GetEnvironmentVariable(key);
			Console.WriteLine("Getting VippsConfiguration value for " + key + " from environment.");
			Console.WriteLine("Key: " + key + " Value: " + value);
			return value;
#else
			return Environment.GetEnvironmentVariable(key);
#endif
		}

		private string GetValueFromObject(string key) {
			foreach (var prop in typeof(VippsConfiguration).GetProperties()) {
				foreach (var attribute in prop.CustomAttributes) {
					foreach (var argument in attribute.ConstructorArguments) {
						if (argument.Value as string == key) {
#if DEBUG
							var value = prop.GetValue(this, null)?.ToString();
							Console.WriteLine("Getting VippsConfiguration value for " + key + " from object.");
							Console.WriteLine("Key: " + key + " Value: " + value);
							return value;
#else
							return prop.GetValue(this, null)?.ToString();
#endif
						}
					}
				}
			}

			return default;
		}
	}
}
