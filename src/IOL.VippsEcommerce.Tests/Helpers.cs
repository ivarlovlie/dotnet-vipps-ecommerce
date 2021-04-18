using System;
using IOL.VippsEcommerce.Models;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Sdk;

namespace IOL.VippsEcommerce.Tests
{
	public static class Helpers
	{
		public static IVippsEcommerceService GetVippsEcommerceService(Action<VippsConfiguration> conf) {
			var services = new ServiceCollection();
			services.AddVippsEcommerceService(conf);
			var provider = services.BuildServiceProvider();
			var vippsEcommerceService = provider.GetService<IVippsEcommerceService>();
			if (vippsEcommerceService == default) {
				throw new NullException(nameof(vippsEcommerceService));
			}

			return vippsEcommerceService;
		}
	}
}
