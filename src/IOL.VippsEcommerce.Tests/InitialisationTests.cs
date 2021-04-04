using IOL.VippsEcommerce.Models;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace IOL.VippsEcommerce.Tests
{
	public class InitialisationTests
	{
		private readonly ITestOutputHelper _helper;

		public InitialisationTests(ITestOutputHelper helper) {
			_helper = helper;
		}

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

		[Fact]
		public void Configuration_Follows_Through_Initialisation() {
			var vippsEcommerceService = Helpers.GetVippsEcommerceService(o => {
				o.ApiUrl = "https://validuri.no";
				o.ClientId = "asdf";
				o.ClientSecret = "asdf";
				o.SecondarySubscriptionKey = "asdf";
				o.PrimarySubscriptionKey = "asdf";
				o.SystemName = "asdf";
				o.SystemVersion = "asdf";
				o.SystemPluginName = "asdf";
				o.SystemPluginVersion = "asdf";
				o.MerchantSerialNumber = "asdf";
				o.CacheDirectoryPath = "asdf";
				o.CacheEncryptionKey = "asdf";
				o.ConfigurationMode = VippsConfigurationMode.ONLY_OBJECT;
			});

			foreach (var prop in typeof(VippsConfiguration).GetProperties()) {
				var value = prop.GetValue(vippsEcommerceService.Configuration, null);
				_helper.WriteLine(prop.Name);
				_helper.WriteLine(value?.ToString() ?? "EMPTY");
				Assert.False(value == default);
			}
		}
	}
}