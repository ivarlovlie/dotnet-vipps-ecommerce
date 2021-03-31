using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api
{
    public class TShippingDetails
    {
        /// <summary>
        /// Gets or Sets Priority
        /// </summary>
        [JsonPropertyName("priority")]
        public int? Priority { get; set; }

        /// <summary>
        /// Gets or Sets ShippingCost
        /// </summary>
        [JsonPropertyName("shippingCost")]
        public double? ShippingCost { get; set; }

        /// <summary>
        /// Shipping method. Max length: 256 characters. Recommended length for readability on most screens: 25 characters.
        /// </summary>
        /// <value>Shipping method. Max length: 256 characters. Recommended length for readability on most screens: 25 characters.</value>
        [JsonPropertyName("shippingMethod")]
        public string ShippingMethod { get; set; }

        /// <summary>
        /// Gets or Sets ShippingMethodId
        /// </summary>
        [JsonPropertyName("shippingMethodId")]
        public string ShippingMethodId { get; set; }
    }
}