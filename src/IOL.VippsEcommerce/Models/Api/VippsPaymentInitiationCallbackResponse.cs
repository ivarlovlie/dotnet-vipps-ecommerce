using System;
using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api
{
    public class VippsPaymentInitiationCallbackResponse
    {
        [JsonPropertyName("merchantSerialNumber")]
        public string MerchantSerialNumber { get; set; }

        [JsonPropertyName("orderId")]
        public string OrderId { get; set; }

        // [JsonPropertyName("shippingDetails")]
        // public TShippingDetails? ShippingDetails { get; set; }

        [JsonPropertyName("transactionInfo")]
        public TTransactionInfo TransactionInfo { get; set; }

        // [JsonPropertyName("userDetails")]
        // public UserDetails? UserDetails { get; set; }
        //
        // [JsonPropertyName("errorInfo")]
        // public TErrorInfo? ErrorInfo { get; set; }


        public class TErrorInfo
        {
            [JsonPropertyName("errorGroup")]
            public string ErrorGroup { get; set; }

            [JsonPropertyName("errorCode")]
            public string ErrorCode { get; set; }

            [JsonPropertyName("errorMessage")]
            public string ErrorMessage { get; set; }

            [JsonPropertyName("contextId")]
            public Guid ContextId { get; set; }
        }

        public class TShippingDetails
        {
            [JsonPropertyName("address")]
            public TAddress Address { get; set; }

            [JsonPropertyName("shippingCost")]
            public int ShippingCost { get; set; }

            [JsonPropertyName("shippingMethod")]
            public string ShippingMethod { get; set; }

            [JsonPropertyName("shippingMethodId")]
            public string ShippingMethodId { get; set; }
        }

        public class TAddress
        {
            [JsonPropertyName("addressLine1")]
            public string AddressLine1 { get; set; }

            [JsonPropertyName("addressLine2")]
            public string AddressLine2 { get; set; }

            [JsonPropertyName("city")]
            public string City { get; set; }

            [JsonPropertyName("country")]
            public string Country { get; set; }

            [JsonPropertyName("zipCode")]
            public string ZipCode { get; set; }
        }

        public class TTransactionInfo
        {
            [JsonPropertyName("amount")]
            public int Amount { get; set; }

            [JsonPropertyName("status")]
            public string Status { get; set; }

            public ETransactionStatus StatusEnum() => Enum.Parse<ETransactionStatus>(Status);

            [JsonPropertyName("timeStamp")]
            public DateTime TimeStamp { get; set; }

            [JsonPropertyName("transactionId")]
            public string TransactionId { get; set; }
        }

        public class TUserDetails
        {
            [JsonPropertyName("bankIdVerified")]
            public string BankIdVerified { get; set; }

            [JsonPropertyName("dateOfBirth")]
            public string DateOfBirth { get; set; }

            [JsonPropertyName("email")]
            public string Email { get; set; }

            [JsonPropertyName("firstName")]
            public string FirstName { get; set; }

            [JsonPropertyName("lastName")]
            public string LastName { get; set; }

            [JsonPropertyName("mobileNumber")]
            public string MobileNumber { get; set; }

            [JsonPropertyName("ssn")]
            public string Ssn { get; set; }

            [JsonPropertyName("userId")]
            public string UserId { get; set; }
        }
    }
}