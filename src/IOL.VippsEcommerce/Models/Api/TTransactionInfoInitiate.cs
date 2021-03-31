using System;
using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api
{
    public class TTransactionInfoInitiate
    {
        /// <summary>
        /// Amount in øre. 32 bit Integer (2147483647). Must be non-zero.
        /// </summary>
        /// <value>Amount in øre. 32 bit Integer (2147483647). Must be non-zero.</value>
        [JsonPropertyName("amount")]
        public int? Amount { get; set; }

        /// <summary>
        /// Id which uniquely identifies a payment. Maximum length is 50 alphanumeric characters: a-z, A-Z, 0-9 and &#x27;-&#x27;.
        /// </summary>
        /// <value>Id which uniquely identifies a payment. Maximum length is 50 alphanumeric characters: a-z, A-Z, 0-9 and &#x27;-&#x27;.</value>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// ISO formatted date time string.
        /// </summary>
        /// <value>ISO formatted date time string.</value>
        [JsonPropertyName("timeStamp")]
        public string TimeStamp { get; set; }

        /// <summary>
        /// Transaction text to be displayed in Vipps
        /// </summary>
        /// <value>Transaction text to be displayed in Vipps</value>
        [JsonPropertyName("transactionText")]
        public string TransactionText { get; set; }

        /// <summary>
        /// Skips the landing page for whitelisted sale units. Requires a valid customerInfo.mobileNumber.
        /// </summary>
        /// <value>Skips the landing page for whitelisted sale units. Requires a valid customerInfo.mobileNumber.</value>
        [JsonPropertyName("skipLandingPage")]
        public bool? SkipLandingPage { get; set; }


        /// <summary>
        /// Gets or Sets AdditionalData
        /// </summary>
        [JsonPropertyName("additionalData")]
        public TAdditionalTransactionData AdditionalData { get; set; }

        /// <summary>
        /// Use the extended UX flow for express checkout which forces users to confirm their address and shipping choices
        /// </summary>
        /// <value>Use the extended UX flow for express checkout which forces users to confirm their address and shipping choices</value>
        [JsonPropertyName("useExplicitCheckoutFlow")]
        public bool? UseExplicitCheckoutFlow { get; set; }
    }
}