# Notes

## Recreate Solution

```powershell
Get-ChildItem *.sln | Remove-Item
dotnet new sln
Get-ChildItem *.csproj -Recurse | ForEach-Object { dotnet sln add $PSItem }
```

## Run SQL Server in Docker Container

```PowerShell
$password = New-Guid
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
$database = "Comics"
$connectionString = "Server=localhost;Database=$database;User Id=sa;Password=$password"
```

## Enable User Secrets

```powershell
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:Comics" "$connectionString"
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.Configuration.UserSecrets
```

## Change Ports

```csharp
webBuilder.UseUrls("http://localhost:5002", "https://localhost:5003");
```
