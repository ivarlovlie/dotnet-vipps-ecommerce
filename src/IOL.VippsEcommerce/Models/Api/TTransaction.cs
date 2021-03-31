using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api
{
    public class TTransaction
    {
        /// <summary>
        /// Amount in øre, if amount is 0 or not provided then full capture will be performed. 32 Bit Integer (2147483647)
        /// </summary>
        /// <value>Amount in øre, if amount is 0 or not provided then full capture will be performed. 32 Bit Integer (2147483647)</value>
        [JsonPropertyName("amount")]
        public int? Amount { get; set; }

        /// <summary>
        /// Transaction text to be displayed in Vipps
        /// </summary>
        /// <value>Transaction text to be displayed in Vipps</value>
        [JsonPropertyName("transactionText")]
        public string TransactionText { get; set; }
    }
}