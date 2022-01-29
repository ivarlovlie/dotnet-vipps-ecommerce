Implements https://vippsas.github.io/vipps-ecom-api , more or less (see [IVippsEcommerceService.cs](https://git.sr.ht/~ivar/IOL.VippsEcommerce/tree/master/item/src/IOL.VippsEcommerce/IVippsEcommerceService.cs)).

`dotnet add package IOL.VippsEcommerce`

[![builds.sr.ht status](https://builds.sr.ht/~ivar/IOL.VippsEcommerce/commits/.run-tests.yml.svg)](https://builds.sr.ht/~ivar/IOL.VippsEcommerce/commits/.run-tests.yml?)
![IOL.VippsEcommerce on nuget.org](https://img.shields.io/badge/target-net5.0-blue)
[![IOL.VippsEcommerce on nuget.org](https://img.shields.io/nuget/v/IOL.VippsEcommerce)](https://www.nuget.org/packages/IOL.VippsEcommerce)
([fuget](https://www.fuget.org/packages/IOL.VippsEcommerce))

## Configuration

Use Dependency Injection to add and configure the service with your values, minimal setup example:
```csharp
services.AddVippsEcommerceService(o => {
	o.ApiUrl = "";
	o.PrimarySubscriptionKey = "";
	o.ClientSecret = "";
	o.ClientId = "";
});
```

See [VippsConfiguration.cs](https://git.sr.ht/~ivar/IOL.VippsEcommerce/tree/master/item/src/IOL.VippsEcommerce/Models/VippsConfiguration.cs) for available properties.

## Caching

The service can cache the credentials for api-access in a file with optional AES encryption, example:
```csharp
services.AddVippsEcommerceService(o => {
	o.CacheEncryptionKey = "randomstring"; // optional key for AES encryption, if omitted the cache will be readable json with your keys exposed and everything.
	o.CacheDirectoryPath = "/tmp/vippsecom"; // path to a directory that the executing process has write-access to.
});
```
