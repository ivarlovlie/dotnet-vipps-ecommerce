using System;
using System.Text.Json;
using IOL.VippsEcommerce;
using IOL.VippsEcommerce.Models;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddVippsEcommerceService(o => {
	o.ConfigurationMode = VippsConfigurationMode.ONLY_ENVIRONMENT;
});
var provider = services.BuildServiceProvider();
var vippsEcommerceService = provider.GetService<IVippsEcommerceService>();
if (vippsEcommerceService == default) {
	return;
}

Console.WriteLine(JsonSerializer.Serialize(vippsEcommerceService.Configuration));