using System.Text.Json.Serialization;

namespace IOL.VippsEcommerce.Models.Api;

/// <summary>
/// VippsErrorResponse
/// </summary>
public class VippsErrorResponse
{
	/// <summary>
	/// The error group. See: https://github.com/vippsas/vipps-ecom-api/blob/master/vipps-ecom-api.md#error-groups
	/// </summary>
	/// <value>The error group. See: https://github.com/vippsas/vipps-ecom-api/blob/master/vipps-ecom-api.md#error-groups</value>
	[JsonPropertyName("errorGroup")]
	public EErrorGroupEnum ErrorGroup { get; }


	/// <summary>
	/// The error code. See: https://github.com/vippsas/vipps-ecom-api/blob/master/vipps-ecom-api.md#error-codes
	/// </summary>
	/// <value>The error code. See: https://github.com/vippsas/vipps-ecom-api/blob/master/vipps-ecom-api.md#error-codes</value>
	[JsonPropertyName("errorCode")]
	public string ErrorCode { get; }

	/// <summary>
	/// A description of what went wrong. See https://github.com/vippsas/vipps-ecom-api/blob/master/vipps-ecom-api.md#errors
	/// </summary>
	/// <value>A description of what went wrong. See https://github.com/vippsas/vipps-ecom-api/blob/master/vipps-ecom-api.md#errors</value>
	[JsonPropertyName("errorMessage")]
	public string ErrorMessage { get; }

	/// <summary>
	/// A unique id for this error, useful for searching in logs
	/// </summary>
	/// <value>A unique id for this error, useful for searching in logs</value>
	[JsonPropertyName("contextId")]
	public string ContextId { get; }
}