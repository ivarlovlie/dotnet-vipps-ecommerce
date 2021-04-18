using System.Threading;
using System.Threading.Tasks;
using IOL.VippsEcommerce.Models;
using IOL.VippsEcommerce.Models.Api;

namespace IOL.VippsEcommerce
{
	public interface IVippsEcommerceService
	{
		public VippsConfiguration Configuration { get; }

		public Task<VippsInitiatePaymentResponse> InitiatePaymentAsync(
				VippsInitiatePaymentRequest payload,
				CancellationToken ct = default
		);

		public Task<VippsPaymentActionResponse> CapturePaymentAsync(
				string orderId,
				VippsPaymentActionRequest payload,
				CancellationToken ct = default
		);

		public Task<VippsPaymentActionResponse> CancelPaymentAsync(
				string orderId,
				VippsPaymentActionRequest payload,
				CancellationToken ct = default
		);

		public Task<VippsPaymentActionResponse> AuthorizePaymentAsync(
				string orderId,
				VippsPaymentActionRequest payload,
				CancellationToken ct = default
		);

		public Task<VippsPaymentActionResponse> RefundPaymentAsync(
				string orderId,
				VippsPaymentActionRequest payload,
				CancellationToken ct = default
		);

		public Task<bool> ForceApprovePaymentAsync(
				string orderId,
				VippsForceApproveRequest payload,
				CancellationToken ct = default
		);

		public Task<VippsGetPaymentDetailsResponse> GetPaymentDetailsAsync(
				string orderId,
				CancellationToken ct = default
		);
	}
}
