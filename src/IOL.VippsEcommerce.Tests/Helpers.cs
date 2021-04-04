using System;
using System.Net;
using System.Text.Json;
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

		public static VippsConfiguration GetVippsValidConfiguration() {
			var json = System.IO.File.ReadAllText("configuration.json");
			var configuration = JsonSerializer.Deserialize<VippsConfiguration>(json);
			return configuration;
		}
	}
}