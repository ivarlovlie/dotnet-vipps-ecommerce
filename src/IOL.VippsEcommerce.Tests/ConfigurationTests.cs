using Xunit;

namespace IOL.VippsEcommerce.Tests;

public class InitialisationTests
{
	[Fact]
	public void Succeed_On_Valid_Minimal_Configuration() {
		var vippsEcommerceService = Helpers.GetVippsEcommerceService(o => {
			o.ApiUrl = "https://validuri.no";
			o.ClientId = "asdf";
			o.ClientSecret = "asdf";
			o.SecondarySubscriptionKey = "asdf";
		});

		vippsEcommerceService.Configuration.Verify();
	}
}
