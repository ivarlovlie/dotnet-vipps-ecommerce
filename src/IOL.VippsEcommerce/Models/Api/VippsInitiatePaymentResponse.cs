using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api;

public class VippsInitiatePaymentResponse
{
	/// <summary>
	/// Id which uniquely identifies a payment. Maximum length is 50 alphanumeric characters: a-z, A-Z, 0-9 and &#x27;-&#x27;.
	/// </summary>
	/// <value>Id which uniquely identifies a payment. Maximum length is 50 alphanumeric characters: a-z, A-Z, 0-9 and &#x27;-&#x27;.</value>
	[JsonPropertyName("orderId")]
	public string OrderId { get; set; }

	/// <summary>
	/// URL to redirect the user to Vipps landing page or a deeplink URL to open Vipps app, if &#x60;isApp&#x60; was set as true. The landing page will also redirect a user to the app if the user is using a mobile browser. This link will timeout after 5 minutes. This example is a shortened deeplink URL. The URL received fromn Vipps should not be changed, and the format may change without notice.
	/// </summary>
	/// <value>URL to redirect the user to Vipps landing page or a deeplink URL to open Vipps app, if &#x60;isApp&#x60; was set as true. The landing page will also redirect a user to the app if the user is using a mobile browser. This link will timeout after 5 minutes. This example is a shortened deeplink URL. The URL received fromn Vipps should not be changed, and the format may change without notice.</value>
	[JsonPropertyName("url")]
	public string Url { get; set; }
}