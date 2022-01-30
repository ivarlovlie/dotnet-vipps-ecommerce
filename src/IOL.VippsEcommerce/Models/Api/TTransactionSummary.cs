using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api;

public class TTransactionSummary
{
	/// <summary>
	/// Total amount captured
	/// </summary>
	/// <value>Total amount captured</value>
	[JsonPropertyName("capturedAmount")]
	public int? CapturedAmount { get; set; }

	/// <summary>
	/// Total refunded amount of the order
	/// </summary>
	/// <value>Total refunded amount of the order</value>
	[JsonPropertyName("refundedAmount")]
	public int? RefundedAmount { get; set; }

	/// <summary>
	/// Total remaining amount to capture
	/// </summary>
	/// <value>Total remaining amount to capture</value>
	[JsonPropertyName("remainingAmountToCapture")]
	public int? RemainingAmountToCapture { get; set; }

	/// <summary>
	/// Total remaining amount to refund
	/// </summary>
	/// <value>Total remaining amount to refund</value>
	[JsonPropertyName("remainingAmountToRefund")]
	public int? RemainingAmountToRefund { get; set; }

	/// <summary>
	/// Bank Identification Number, first 6 digit of card number
	/// </summary>
	/// <value>Bank Identification Number, first 6 digit of card number</value>
	[JsonPropertyName("bankIdentificationNumber")]
	public string BankIdentificationNumber { get; set; }
}