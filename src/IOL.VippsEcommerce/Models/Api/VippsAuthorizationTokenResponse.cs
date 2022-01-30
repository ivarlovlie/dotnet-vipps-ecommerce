using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api;

/// <summary>
/// AuthorizationTokenResponse
/// </summary>
public class VippsAuthorizationTokenResponse
{
	/// <summary>
	/// String containing the type for the Access Token.
	/// </summary>
	/// <value>String containing the type for the Access Token.</value>
	[JsonPropertyName("token_type")]
	public string TokenType { get; set; }

	/// <summary>
	/// Token expiry time in seconds.
	/// </summary>
	/// <value>Token expiry time in seconds.</value>
	[JsonPropertyName("expires_in")]
	public string ExpiresIn { get; set; }

	/// <summary>
	/// Extra time added to expiry time. Currently disabled.
	/// </summary>
	/// <value>Extra time added to expiry time. Currently disabled.</value>
	[JsonPropertyName("ext_expires_in")]
	public string ExtExpiresIn { get; set; }

	/// <summary>
	/// Token expiry time in epoch time format.
	/// </summary>
	/// <value>Token expiry time in epoch time format.</value>
	[JsonPropertyName("expires_on")]
	public string ExpiresOn { get; set; }

	/// <summary>
	/// Token creation time in epoch time format.
	/// </summary>
	/// <value>Token creation time in epoch time format.</value>
	[JsonPropertyName("not_before")]
	public string NotBefore { get; set; }

	/// <summary>
	/// A common resource object. Not used in token validation
	/// </summary>
	/// <value>A common resource object. Not used in token validation</value>
	[JsonPropertyName("resource")]
	public string Resource { get; set; }

	/// <summary>
	/// Gets or Sets AccessToken
	/// </summary>
	[JsonPropertyName("access_token")]
	public string AccessToken { get; set; }
}