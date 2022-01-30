using System;
using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api;

public class VippsGetPaymentDetailsResponse
{
	[JsonPropertyName("orderId")]
	public string OrderId { get; set; }

	[JsonPropertyName("shippingDetails")]
	public ShippingDetails ShippingDetails { get; set; }

	[JsonPropertyName("transactionLogHistory")]
	public TransactionLogHistory[] TransactionLogHistory { get; set; }

	[JsonPropertyName("transactionSummary")]
	public TransactionSummary TransactionSummary { get; set; }

	[JsonPropertyName("userDetails")]
	public UserDetails UserDetails { get; set; }
}

public class ShippingDetails
{
	[JsonPropertyName("address")]
	public Address Address { get; set; }

	[JsonPropertyName("shippingCost")]
	public long ShippingCost { get; set; }

	[JsonPropertyName("shippingMethod")]
	public string ShippingMethod { get; set; }

	[JsonPropertyName("shippingMethodId")]
	public string ShippingMethodId { get; set; }
}

public class Address
{
	[JsonPropertyName("addressLine1")]
	public string AddressLine1 { get; set; }

	[JsonPropertyName("addressLine2")]
	public string AddressLine2 { get; set; }

	[JsonPropertyName("city")]
	public string City { get; set; }

	[JsonPropertyName("country")]
	public string Country { get; set; }

	[JsonPropertyName("postCode")]
	public string PostCode { get; set; }
}

public class TransactionLogHistory
{
	[JsonPropertyName("amount")]
	public long Amount { get; set; }

	[JsonPropertyName("operation")]
	public string Operation { get; set; }

	[JsonPropertyName("operationSuccess")]
	public bool OperationSuccess { get; set; }

	[JsonPropertyName("requestId")]
	public string RequestId { get; set; }

	[JsonPropertyName("timeStamp")]
	public DateTime TimeStamp { get; set; }

	[JsonPropertyName("transactionId")]
	public string TransactionId { get; set; }

	[JsonPropertyName("transactionText")]
	public string TransactionText { get; set; }
}

public class TransactionSummary
{
	[JsonPropertyName("capturedAmount")]
	public long CapturedAmount { get; set; }

	[JsonPropertyName("refundedAmount")]
	public long RefundedAmount { get; set; }

	[JsonPropertyName("remainingAmountToCapture")]
	public long RemainingAmountToCapture { get; set; }

	[JsonPropertyName("remainingAmountToRefund")]
	public long RemainingAmountToRefund { get; set; }

	[JsonPropertyName("bankIdentificationNumber")]
	public long BankIdentificationNumber { get; set; }
}

public class UserDetails
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
	public long MobileNumber { get; set; }

	[JsonPropertyName("ssn")]
	public string Ssn { get; set; }

	[JsonPropertyName("userId")]
	public string UserId { get; set; }
}