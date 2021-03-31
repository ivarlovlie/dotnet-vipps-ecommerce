using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api
{
    public class VippsForceApproveRequest
    {
        /// <summary>
        /// Target customer phone number. 8 digits.
        /// </summary>
        /// <value>Target customer phone number. 8 digits.</value>
        [JsonPropertyName("customerPhoneNumber")]
        public string CustomerPhoneNumber { get; set; }

        /// <summary>
        /// The token value recieved in the &#x60;url&#x60; property in the Initiate response
        /// </summary>
        /// <value>The token value recieved in the &#x60;url&#x60; property in the Initiate response</value>
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}