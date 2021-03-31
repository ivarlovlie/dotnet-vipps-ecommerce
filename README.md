## Install
`dotnet add package IOL.VippsEcommerce`

The service targets net5.0. \
[Nuget page](https://www.nuget.org/packages/IOL.VippsEcommerce/)
[Fuget page](https://www.fuget.org/packages/IOL.VippsEcommerce/)

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
You can also use environment variables to configure the service, example:
```csharp
services.AddVippsEcommerceService(o => {
	o.UseEnvironment = true;
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
