$project = "MyApp.Server"
$password = New-Guid

Write-Host "Starting SQL Server"
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
$database = "Comics"
$connectionString = "Server=localhost;Database=$database;User Id=sa;Password=$password;Trusted_Connection=False;Encrypt=False"

Write-Host "Configuring Connection String"
dotnet user-secrets init --project $project
dotnet user-secrets set "ConnectionStrings:Comics" "$connectionString" --project $project

Write-Host "Starting Azurite Storage Emulator"
$volume = Resolve-Path ".local/temp"
docker run -p 10000:10000 -p 10001:10001 -p 10002:10002 -v "${volume}:/data" -d mcr.microsoft.com/azure-storage/azurite

Write-Host "Starting App"
dotnet run --project $project
