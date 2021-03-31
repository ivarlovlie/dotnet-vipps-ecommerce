using System.Text.Json.Serialization;
using IOL.VippsEcommerce.Models.Api;

namespace IOL.VippsEcommerce.Models.Api
{
	public class TTransactionInfo
	{
		/// <summary>
		/// Status which gives the current state of the payment within Vipps. See the [API guide](https://github.com/vippsas/vipps-ecom-api/blob/master/vipps-ecom-api.md#responses-from-requests) for more information.
		/// </summary>
		/// <value>Status which gives the current state of the payment within Vipps. See the [API guide](https://github.com/vippsas/vipps-ecom-api/blob/master/vipps-ecom-api.md#responses-from-requests) for more information.</value>
		[JsonPropertyName("status")]
		public EStatusEnum Status { get; set; }
	}
}