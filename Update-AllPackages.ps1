$regex = 'PackageReference Include="([^"]*)" Version="([^"]*)"'

Get-ChildItem -Recurse | Where-Object Extension -Like "*proj" | ForEach-Object {
    $projectName = $PSItem.Name
    $project = $PSItem.FullName
    Get-Content -Path $project |
    Select-String -Pattern $regex |
    ForEach-Object {
        $package = $PSItem.Matches.Groups[1].Value
        Write-Host -Object "Updating $package in $projectName..." -ForegroundColor Green
        Invoke-Expression -Command "dotnet add `"$project`" package $package"
    }
}
