Implements https://vippsas.github.io/vipps-ecom-api/, more or less.

## Install
`dotnet add package IOL.VippsEcommerce`

The service targets net5.0. \
[Nuget page](https://nuget.org/packages/IOL.VippsEcommerce/)
[Fuget page](https://fuget.org/packages/IOL.VippsEcommerce/)

## Configuration

Use DI to add and configure the service to your liking, example:
```csharp
services.AddVippsEcommerceService(o => {
	o.ApiUrl = "";
	o.PrimarySubscriptionKey = "";
	o.ClientSecret = "";
	o.ClientId = "";
});
```

See [VippsConfiguration.cs](https://github.com/ivarlovlie/IOL.VippsEcommerce/blob/master/src/IOL.VippsEcommerce/Models/VippsConfiguration.cs) for available properties.
You can configure how to get values with the `ConfigurationMode` property, valid modes is specified in [VippsConfigurationMode.cs](https://github.com/ivarlovlie/IOL.VippsEcommerce/blob/master/src/IOL.VippsEcommerce/Models/VippsConfigurationMode.cs), example:
```csharp
services.AddVippsEcommerceService(o => {
	o.ConfigurationMode = VippsConfigurationMode.ENVIRONMENT_THEN_OBJECT;
});
```

With the above example, the service will look for configuration values in the current environment using names specified in [VippsConfigurationKeyNames.cs](https://github.com/ivarlovlie/IOL.VippsEcommerce/blob/master/src/IOL.VippsEcommerce/Models/VippsConfigurationKeyNames.cs). The environment variable name for a given property is also specified in it's XML-documentation.


## Caching

The service can cache the credentials for api-access in a file with optional AES encryption, example:
```csharp
services.AddVippsEcommerceService(o => {
	o.CacheEncryptionKey = "randomstring"; // optional key for AES encryption, if omitted the cache will be readable json with your keys exposed and everything.
	o.CacheDirectoryPath = "/tmp/vippsecom"; // path to a directory that the executing process has write-access to.
});
```
