Implements https://vippsas.github.io/vipps-ecom-api, more or less (see [IVippsEcommerceService.cs](/src/IOL.VippsEcommerce/IVippsEcommerceService.cs)).

Install: `dotnet add package IOL.VippsEcommerce`

Nuget: [https://www.nuget.org/packages/IOL.VippsEcommerce](https://www.nuget.org/packages/IOL.VippsEcommerce)

## Setup

Use Dependency Injection to add and configure the service with your values, minimal setup example:
```csharp
services.AddVippsEcommerceService(o => {
	o.ApiUrl = "";
	o.PrimarySubscriptionKey = "";
	o.ClientSecret = "";
	o.ClientId = "";
});
```

See [VippsConfiguration.cs](/src/IOL.VippsEcommerce/Models/VippsConfiguration.cs) for available properties.

### Caching

The service can cache the credentials for api access in a file with optional AES encryption, example:
```csharp
services.AddVippsEcommerceService(o => {
	o.CacheEncryptionKey = "randomstring"; // optional key for AES encryption, if omitted the cache will be readable json with your keys exposed and everything.
	o.CacheDirectoryPath = "/tmp/vippsecom"; // path to a directory that the executing process has write-access to.
});
```
