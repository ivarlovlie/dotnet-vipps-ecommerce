Implements https://vippsas.github.io/vipps-ecom-api/, more or less (see [IVippsEcommerceService.cs](https://git.sr.ht/~ivar/IOL.VippsEcommerce/tree/master/item/src/IOL.VippsEcommerce/VippsEcommerceService.cs)).

`dotnet add package IOL.VippsEcommerce`

![IOL.VippsEcommerce on nuget.org](https://img.shields.io/badge/target-net5.0-blue)
[![IOL.VippsEcommerce on nuget.org](https://img.shields.io/nuget/v/IOL.VippsEcommerce)](https://www.nuget.org/packages/IOL.VippsEcommerce)
([fuget](https://www.fuget.org/packages/IOL.VippsEcommerce))

## Configuration

Use Dependency Injection to add and configure the service to your liking, example:
```csharp
services.AddVippsEcommerceService(o => {
	o.ApiUrl = "";
	o.PrimarySubscriptionKey = "";
	o.ClientSecret = "";
	o.ClientId = "";
});
```

See [VippsConfiguration.cs](https://git.sr.ht/~ivar/IOL.VippsEcommerce/tree/master/item/src/IOL.VippsEcommerce/Models/VippsConfiguration.cs) for available properties.
You can configure how to get values with the `ConfigurationMode` property, valid modes is specified in [VippsConfigurationMode.cs](https://git.sr.ht/~ivar/IOL.VippsEcommerce/tree/master/item/src/IOL.VippsEcommerce/Models/VippsConfigurationMode.cs), example:
```csharp
services.AddVippsEcommerceService(o => {
	o.ConfigurationMode = VippsConfigurationMode.ENVIRONMENT_THEN_OBJECT;
});
```

With the above example, the service will look for configuration values in the current environment using names specified in [VippsConfigurationKeyNames.cs](https://git.sr.ht/~ivar/IOL.VippsEcommerce/tree/master/item/src/IOL.VippsEcommerce/Models/VippsConfigurationKeyNames.cs), then in the configuration object. The environment variable name for a given property is also specified in it's XML-documentation.


> [Environment.GetEnvironmentVariable](https://docs.microsoft.com/en-us/dotnet/api/system.environment.getenvironmentvariable?view=net-5.0) is used to retrieve environment variables, that means that user-secrets and anything else than process-spesific variables (`VARIABLE=value dotnet YourBinary.dll`) does not register on Unix systems.


## Caching

The service can cache the credentials for api-access in a file with optional AES encryption, example:
```csharp
services.AddVippsEcommerceService(o => {
	o.CacheEncryptionKey = "randomstring"; // optional key for AES encryption, if omitted the cache will be readable json with your keys exposed and everything.
	o.CacheDirectoryPath = "/tmp/vippsecom"; // path to a directory that the executing process has write-access to.
});
```
