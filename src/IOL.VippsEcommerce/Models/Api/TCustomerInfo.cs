using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api;

public class TCustomerInfo
{
	/// <summary>
	/// Mobile number of the user who has to pay for the transation from Vipps. Allowed format: xxxxxxxx. No country code.
	/// </summary>
	/// <value>Mobile number of the user who has to pay for the transation from Vipps. Allowed format: xxxxxxxx. No country code.</value>
	[JsonPropertyName("mobileNumber")]
	public string MobileNumber { get; set; }
}