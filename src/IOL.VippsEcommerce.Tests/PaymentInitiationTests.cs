using Xunit.Abstractions;

namespace IOL.VippsEcommerce.Tests
{
	public class PaymentInitiationTests
	{
		private readonly ITestOutputHelper _helper;

		public PaymentInitiationTests(ITestOutputHelper helper) {
			_helper = helper;
		}
	}
}