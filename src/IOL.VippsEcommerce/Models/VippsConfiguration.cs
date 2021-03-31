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
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.VIPPS_API_URL)]
		public string ApiUrl { get; set; }

		/// <summary>
		/// Client ID for the merchant (the "username"). This property is required.
		/// <para>Corresponding environment variable name: VIPPS_CLIENT_ID</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.VIPPS_CLIENT_ID)]
		public string ClientId { get; set; }

		/// <summary>
		/// Client Secret for the merchant (the "password"). This property is required.
		/// <para>Corresponding environment variable name: VIPPS_CLIENT_SECRET</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.VIPPS_CLIENT_SECRET)]
		public string ClientSecret { get; set; }

		/// <summary>
		///	Primary subscription key for the API product.
		/// <para>The primary subscription key take precedence over the secondary subscription key.</para>
		/// <para>Either primary subscription key or secondary subscription key is required.</para>
		/// <para>Corresponding environment variable name: VIPPS_SUBSCRIPTION_KEY_PRIMARY</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.VIPPS_SUBSCRIPTION_KEY_PRIMARY)]
		public string PrimarySubscriptionKey { get; set; }

		/// <summary>
		///	Secondary subscription key for the API product.
		/// <para>The primary subscription key take precedence over the secondary subscription key.</para>
		/// <para>Either primary subscription key or secondary subscription key is required.</para>
		/// <para>Corresponding environment variable name: VIPPS_SUBSCRIPTION_KEY_SECONDARY</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.VIPPS_SUBSCRIPTION_KEY_SECONDARY)]
		public string SecondarySubscriptionKey { get; set; }

		/// <summary>
		/// The Merchant Serial Number (MSN) is a unique id for the sale unit that this payment is made for.
		/// <para>Corresponding environment variable name: VIPPS_MSN</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.VIPPS_MSN)]
		public string MerchantSerialNumber { get; set; }

		/// <summary>
		/// The name of the ecommerce solution. One word in lowercase letters is good.
		/// <para>Corresponding environment variable name: VIPPS_SYSTEM_NAME</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.VIPPS_SYSTEM_NAME)]
		public string SystemName { get; set; }

		/// <summary>
		/// The version number of the ecommerce solution.
		/// <para>Corresponding environment variable name: VIPPS_SYSTEM_VERSION</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.VIPPS_SYSTEM_VERSION)]
		public string SystemVersion { get; set; }

		/// <summary>
		/// The name of the ecommerce plugin (if applicable). One word in lowercase letters is good.
		/// <para>Corresponding environment variable name: VIPPS_SYSTEM_PLUGIN_NAME</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.VIPPS_SYSTEM_PLUGIN_NAME)]
		public string SystemPluginName { get; set; }

		/// <summary>
		/// The version number of the ecommerce plugin (if applicable).
		/// <para>Corresponding environment variable name: VIPPS_SYSTEM_PLUGIN_VERSION</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.VIPPS_SYSTEM_PLUGIN_VERSION)]
		public string SystemPluginVersion { get; set; }

		/// <summary>
		/// Optional path to a writable directory wherein a credential cache file can be placed.
		/// <para>Corresponding environment variable name: VIPPS_CACHE_PATH</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.VIPPS_CACHE_PATH)]
		public string CacheDirectoryPath { get; set; }

		/// <summary>
		/// Optional key for AES encryption of the credential cache file.
		/// <para>Corresponding environment variable name: VIPPS_CACHE_KEY</para>
		/// </summary>
		[VippsConfigurationKeyName(VippsConfigurationKeyNames.VIPPS_CACHE_KEY)]
		public string CacheEncryptionKey { get; set; }

		/// <summary>
		/// Use environment variables for configuration.
		/// <para>If this is true, all requested properties are looked for in the environment.</para>
		/// </summary>
		public bool UseEnvironment { get; set; }

		/// <summary>
		/// Get value from configuration, either from Dependency injection or from the environment.
		/// </summary>
		/// <param name="key">Configuration key.</param>
		/// <param name="fallback">Fallback value if the key is not found or empty.</param>
		/// <returns>A string containing the configuration value (or a fallback).</returns>
		public string GetValue(string key, string fallback = default) {
			if (UseEnvironment) {
				return Environment.GetEnvironmentVariable(key) ?? fallback;
			}

			foreach (var prop in typeof(VippsConfiguration).GetProperties()) {
				foreach (var attribute in prop.CustomAttributes) {
					foreach (var argument in attribute.ConstructorArguments) {
						if (argument.Value as string == key) {
#if DEBUG
							var value = prop.GetValue(this, null)?.ToString();
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