using System;
using IOL.VippsEcommerce.Models;
using Microsoft.Extensions.DependencyInjection;

namespace IOL.VippsEcommerce
{
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Configures and adds the VippsEcommerceService to your DI. 
		/// </summary>
		/// <param name="services">Servicecollection to add VippsEcommerceService to.</param>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static IServiceCollection AddVippsEcommerceService(
				this IServiceCollection services,
				Action<VippsConfiguration> configuration
		) {
			if (services == null) {
				throw new ArgumentNullException(nameof(services));
			}

			if (configuration == null) {
				throw new ArgumentNullException(nameof(configuration));
			}

			services.Configure(configuration);
			services.AddHttpClient<IVippsEcommerceService, VippsEcommerceService>();
			services.AddScoped<IVippsEcommerceService, VippsEcommerceService>();
			return services;
		}

		/// <summary>
		/// Adds the VippsEcommerceService to your DI, and expects configuration values from environment variables.
		/// </summary>
		/// <param name="services">Servicecollection to add VippsEcommerceService to.</param>
		/// <returns></returns>
		public static IServiceCollection AddVippsEcommerceService(this IServiceCollection services) {
			if (services == null) {
				throw new ArgumentNullException(nameof(services));
			}

			services.Configure(new Action<VippsConfiguration>(o => o.ConfigurationMode = VippsConfigurationMode.ONLY_ENVIRONMENT));
			services.AddHttpClient<IVippsEcommerceService, VippsEcommerceService>();
			services.AddScoped<IVippsEcommerceService, VippsEcommerceService>();
			return services;
		}
	}
}
