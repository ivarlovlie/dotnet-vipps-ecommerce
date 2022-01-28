using System;
using System.Text.Json;
using IOL.VippsEcommerce;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddVippsEcommerceService(o => {
	o.ClientSecret = "asdf";
	o.ClientId = "asdf";
	o.ApiUrl = "sadf";
	o.PrimarySubscriptionKey = "";
	o.Verify();
});
var provider = services.BuildServiceProvider();
var vippsEcommerceService = provider.GetService<IVippsEcommerceService>();
if (vippsEcommerceService == default) {
	return;
}

Console.WriteLine(JsonSerializer.Serialize(vippsEcommerceService.Configuration));
