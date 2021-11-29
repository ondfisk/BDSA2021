# Notes

## Recreate Solution

```powershell
Get-ChildItem *.sln | Remove-Item
dotnet new sln
Get-ChildItem *.csproj -Recurse | ForEach-Object { dotnet sln add $PSItem }
```

## Run SQL Server in Docker Container

```powershell
$password = New-Guid
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
$database = "Comics"
$connectionString = "Server=localhost;Database=$database;User Id=sa;Password=$password;Trusted_Connection=False;Encrypt=False"
```

## Enable User Secrets

```powershell
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:Comics" "$connectionString"
```

## Change Ports

```csharp
webBuilder.UseUrls("http://localhost:5002", "https://localhost:5003");
```

## Configure Auth

```powershell
dotnet tool install Microsoft.dotnet-msidentity --global
$tenantId = "b461d90e-0c15-44ec-adc2-51d14f9f5731"
$appDisplayName = "MyApp"
dotnet msidentity --create-app-registration --tenant-id $tenantId --app-display-name $appDisplayName
$clientId = "ad4f8934-36c8-46e9-a100-785353381a40"
dotnet msidentity --register-app --tenant-id $tenantId --client-id $clientId
```

## Azurite Storage Emulator

```powershell
$volume = Resolve-Path ".local/temp"
docker run -p 10000:10000 -p 10001:10001 -p 10002:10002 -v "${volume}:/data" mcr.microsoft.com/azure-storage/azurite
```
