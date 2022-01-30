using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api;

public class VippsInitiatePaymentRequest
{
	/// <summary>
	/// Gets or Sets CustomerInfo
	/// </summary>
	[JsonPropertyName("customerInfo")]
	public TCustomerInfo CustomerInfo { get; set; }

	/// <summary>
	/// Gets or Sets MerchantInfo
	/// </summary>
	///
	[JsonPropertyName("merchantInfo")]
	public TMerchantInfo MerchantInfo { get; set; }

	/// <summary>
	/// Gets or Sets Transaction
	/// </summary>
	[JsonPropertyName("transaction")]
	public TTransactionInfoInitiate Transaction { get; set; }

}