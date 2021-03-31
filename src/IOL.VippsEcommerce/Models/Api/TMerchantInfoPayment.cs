using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api
{
    public class TMerchantInfoPayment
    {
                
        /// <summary>
        /// Unique id for this merchant&#x27;s sales channel: website, mobile app etc. Short name: MSN.
        /// </summary>
        /// <value>Unique id for this merchant&#x27;s sales channel: website, mobile app etc. Short name: MSN.</value>
        [JsonPropertyName("merchantSerialNumber")]
        public string MerchantSerialNumber { get; set; }
    }
}