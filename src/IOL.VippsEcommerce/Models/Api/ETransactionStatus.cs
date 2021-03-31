using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api
{
    public enum ETransactionStatus
    {
        [JsonPropertyName("RESERVED")]
        RESERVED,
        [JsonPropertyName("SALE")]
        SALE,
        [JsonPropertyName("CANCELLED")]
        CANCELLED,
        [JsonPropertyName("REJECTED")]
        REJECTED,
        [JsonPropertyName("AUTO_CANCEL")]
        AUTO_CANCEL,
        [JsonPropertyName("SALE_FAILED")]
        SALE_FAILED,
        [JsonPropertyName("RESERVE_FAILED")]
        RESERVE_FAILED
    }
}