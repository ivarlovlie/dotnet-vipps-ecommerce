using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api
{
    public enum EStatusEnum
    {
        /// <summary>
        /// Enum Cancelled for value: Cancelled
        /// </summary>
        [JsonPropertyName("Cancelled")]
        CANCELLED = 1,

        /// <summary>
        /// Enum Captured for value: Captured
        /// </summary>
        [JsonPropertyName("Captured")]
        CAPTURED = 2,

        /// <summary>
        /// Enum Refund for value: Refund
        /// </summary>
        [JsonPropertyName("Refund")]
        REFUND = 3
    }
}