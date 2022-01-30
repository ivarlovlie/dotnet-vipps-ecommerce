using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api;

public class TMerchantInfo
{
	/// <summary>
	/// Authorization token that the merchant could share to make callbacks more secure. If provided this token will be returned as an &#x60;Authorization&#x60; header for our callbacks. This includes shipping details and callback.
	/// </summary>
	/// <value>Authorization token that the merchant could share to make callbacks more secure. If provided this token will be returned as an &#x60;Authorization&#x60; header for our callbacks. This includes shipping details and callback.</value>
	//[JsonPropertyName("authToken")]
	//public string AuthToken { get; set; }

	/// <summary>
	/// This is an URL for Vipps to call at the merchant&#x27;s server to provide updated information about the order after the payment request. Domain name and context path should be provided by merchant as the value for this parameter. Vipps will add &#x60;/v2/payments/{orderId}&#x60; to the end or this URL. URLs passed to Vipps must validate with the Apache Commons [UrlValidator](https://commons.apache.org/proper/commons-validator/apidocs/org/apache/commons/validator/routines/UrlValidator.html). We don&#x27;t send requests to all ports, so to be safe use common ports such as: 80, 443, 8080.
	/// </summary>
	/// <value>This is an URL for Vipps to call at the merchant&#x27;s server to provide updated information about the order after the payment request. Domain name and context path should be provided by merchant as the value for this parameter. Vipps will add &#x60;/v2/payments/{orderId}&#x60; to the end or this URL. URLs passed to Vipps must validate with the Apache Commons [UrlValidator](https://commons.apache.org/proper/commons-validator/apidocs/org/apache/commons/validator/routines/UrlValidator.html). We don&#x27;t send requests to all ports, so to be safe use common ports such as: 80, 443, 8080.</value>
	[JsonPropertyName("callbackPrefix")]
	public string CallbackPrefix { get; set; }

	/// <summary>
	/// Required for Vipps Hurtigkasse (express checkout) payments. This callback URL will be used by Vipps to inform the merchant that the user has revoked his/her consent: This Vipps user does do not want the merchant to store or use his/her personal information anymore. Required by GDPR. Vipps will add &#x60;/v2/consents/{userId}&#x60; to the end or this URL. URLs passed to Vipps should be URL-encoded, and must validate with the Apache Commons [UrlValidator](https://commons.apache.org/proper/commons-validator/apidocs/org/apache/commons/validator/routines/UrlValidator.html). We don&#x27;t send requests to all ports, so to be safe use common ports such as: 80, 443, 8080.
	/// </summary>
	/// <value>Required for Vipps Hurtigkasse (express checkout) payments. This callback URL will be used by Vipps to inform the merchant that the user has revoked his/her consent: This Vipps user does do not want the merchant to store or use his/her personal information anymore. Required by GDPR. Vipps will add &#x60;/v2/consents/{userId}&#x60; to the end or this URL. URLs passed to Vipps should be URL-encoded, and must validate with the Apache Commons [UrlValidator](https://commons.apache.org/proper/commons-validator/apidocs/org/apache/commons/validator/routines/UrlValidator.html). We don&#x27;t send requests to all ports, so to be safe use common ports such as: 80, 443, 8080.</value>
	//[JsonPropertyName("consentRemovalPrefix")]
	//public string ConsentRemovalPrefix { get; set; }

	/// <summary>
	/// Vipps will use the fallBack URL to redirect the Vipps user to the merchant’s confirmation page once the payment is completed in Vipps. This is normally the “success page”, although the “fallback” name is ambiguous (the same URL is also used if payment was not successful). In other words: This is the URL Vipps sends the Vipps user back to. URLs passed to Vipps must validate with the Apache Commons [UrlValidator](https://commons.apache.org/proper/commons-validator/apidocs/org/apache/commons/validator/routines/UrlValidator.html).
	/// </summary>
	/// <value>Vipps will use the fallBack URL to redirect the Vipps user to the merchant’s confirmation page once the payment is completed in Vipps. This is normally the “success page”, although the “fallback” name is ambiguous (the same URL is also used if payment was not successful). In other words: This is the URL Vipps sends the Vipps user back to. URLs passed to Vipps must validate with the Apache Commons [UrlValidator](https://commons.apache.org/proper/commons-validator/apidocs/org/apache/commons/validator/routines/UrlValidator.html).</value>
	[JsonPropertyName("fallBack")]
	public string FallBack { get; set; }

	/// <summary>
	/// This parameter indicates whether payment request is triggered from Mobile App or Web browser. Based on this value, response will be redirect URL for Vipps landing page or deeplink URL to connect vipps App. When isApp is set to true, URLs passed to Vipps will not be validated as regular URLs.
	/// </summary>
	/// <value>This parameter indicates whether payment request is triggered from Mobile App or Web browser. Based on this value, response will be redirect URL for Vipps landing page or deeplink URL to connect vipps App. When isApp is set to true, URLs passed to Vipps will not be validated as regular URLs.</value>
	[JsonPropertyName("isApp")]
	public bool? IsApp { get; set; }

	/// <summary>
	/// Unique id for this merchant&#x27;s sales channel: website, mobile app etc. Short name: MSN.
	/// </summary>
	/// <value>Unique id for this merchant&#x27;s sales channel: website, mobile app etc. Short name: MSN.</value>
	[JsonPropertyName("merchantSerialNumber")]
	public string MerchantSerialNumber { get; set; }


	/// <summary>
	/// In case of Vipps Hurtigkasse (express checkout) payment, merchant should pass this prefix to let Vipps fetch shipping cost and method related details. Vipps will add &#x60;/v2/payments/{orderId}/shippingDetails&#x60; to the end or this URL. We don&#x27;t send requests to all ports, so to be safe use common ports such as: 80, 443, 8080.
	/// </summary>
	/// <value>In case of Vipps Hurtigkasse (express checkout) payment, merchant should pass this prefix to let Vipps fetch shipping cost and method related details. Vipps will add &#x60;/v2/payments/{orderId}/shippingDetails&#x60; to the end or this URL. We don&#x27;t send requests to all ports, so to be safe use common ports such as: 80, 443, 8080.</value>
	[JsonPropertyName("shippingDetailsPrefix")]
	public string ShippingDetailsPrefix { get; set; }

	/// <summary>
	/// If shipping method and cost are always a fixed value, for example 50 NOK, then the method and price can be provided during the initiate call. The shippingDetailsPrefix callback will not be used if this value is provided. This will result in a faster checkout and a better customer experience.
	/// </summary>
	/// <value>If shipping method and cost are always a fixed value, for example 50 NOK, then the method and price can be provided during the initiate call. The shippingDetailsPrefix callback will not be used if this value is provided. This will result in a faster checkout and a better customer experience.</value>
	[JsonPropertyName("staticShippingDetails")]
	public List<TShippingDetails> StaticShippingDetails { get; set; }
}