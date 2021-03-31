using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api
{
    /// <summary>
    /// The error group. See: https://github.com/vippsas/vipps-ecom-api/blob/master/vipps-ecom-api.md#error-groups
    /// </summary>
    public enum EErrorGroupEnum
    {
        /// <summary>
        /// Enum Authentication for value: Authentication
        /// </summary>
        [JsonPropertyName("Authentication")]
        AUTHENTICATION = 1,

        /// <summary>
        /// Enum Payments for value: Payments
        /// </summary>
        [JsonPropertyName("Payments")]
        PAYMENTS = 2,

        /// <summary>
        /// Enum InvalidRequest for value: InvalidRequest
        /// </summary>
        [JsonPropertyName("InvalidRequest")]
        INVALID_REQUEST = 3,

        /// <summary>
        /// Enum VippsError for value: VippsError
        /// </summary>
        [JsonPropertyName("VippsError")]
        VIPPS_ERROR = 4,

        /// <summary>
        /// Enum User for value: User
        /// </summary>
        [JsonPropertyName("User")]
        USER = 5,

        /// <summary>
        /// Enum Merchant for value: Merchant
        /// </summary>
        [JsonPropertyName("Merchant")]
        MERCHANT = 6

    }
}