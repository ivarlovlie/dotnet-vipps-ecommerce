using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api
{
    public class VippsPaymentActionResponse
    {
        /// <summary>
        /// Text which describes what instrument was used to complete the payment. Not included until a user has chosen and approved in the app.
        /// </summary>
        /// <value>Text which describes what instrument was used to complete the payment. Not included until a user has chosen and approved in the app.</value>
        [JsonPropertyName("paymentInstrument")]
        public string PaymentInstrument { get; set; }

        /// <summary>
        /// Id which uniquely identifies a payment. Maximum length is 50 alphanumeric characters: a-z, A-Z, 0-9 and &#x27;-&#x27;.
        /// </summary>
        /// <value>Id which uniquely identifies a payment. Maximum length is 50 alphanumeric characters: a-z, A-Z, 0-9 and &#x27;-&#x27;.</value>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or Sets TransactionInfo
        /// </summary>
        [JsonPropertyName("transactionInfo")]
        public TTransactionInfo TransactionInfo { get; set; }

        /// <summary>
        /// Gets or Sets TransactionSummary
        /// </summary>
        [JsonPropertyName("transactionSummary")]
        public TTransactionSummary TransactionSummary { get; set; }

    }
}