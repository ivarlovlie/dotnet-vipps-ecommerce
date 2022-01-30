using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api;

public class VippsPaymentActionRequest
{
	/// <summary>
	/// Gets or Sets MerchantInfo
	/// </summary>
	[JsonPropertyName("merchantInfo")]
	public TMerchantInfoPayment MerchantInfo { get; set; }

	/// <summary>
	/// Gets or Sets Transaction
	/// </summary>
	[JsonPropertyName("transaction")]
	public TTransaction Transaction { get; set; }
}