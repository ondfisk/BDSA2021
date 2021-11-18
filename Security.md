# Security

## Authentication

Source: <https://docs.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/hosted-with-azure-active-directory?view=aspnetcore-6.0>

```powershell
# Server
$serverApiClientId = "8bfdad82-6c9e-41a2-bd1a-83d65103e5e1"
$tenantId = "b461d90e-0c15-44ec-adc2-51d14f9f5731"
$tenantDomain = "ondfisk.dk"
$appIdUri = "api://8bfdad82-6c9e-41a2-bd1a-83d65103e5e1/API.Access"
$scope = "API.Access"

## Client
$redirectUri = "https =//localhost =7207/authentication/login-callback"
$clientAppClientId = "5f16394d-16c5-416e-8265-5de99ce6d457"

dotnet new blazorwasm --hosted `
    --auth SingleOrg `
    --api-client-id "$serverApiClientId" `
    --app-id-uri "$serverApiClientId" `
    --client-id "$clientAppClientId" `
    --default-scope "$scope" `
    --domain "$tenantDomain" `
    --output "MyApp" `
    --tenant-id "$tenantId"
```

## Certificates

Source: <https://docs.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-6.0>

```powershell
dotnet dev-certs https --clean
dotnet dev-certs https --export-path $env:USERPROFILE\.aspnet\https\aspnetapp.pfx --password localhost --trust
dotnet dev-certs https --trust
```
