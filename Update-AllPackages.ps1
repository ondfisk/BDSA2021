Get-ChildItem -Recurse | Where-Object Extension -Like "*proj" | ForEach-Object {
    $projectName = $PSItem.Name
    $path = $PSItem.FullName
    [xml]$project = Get-Content -Path $path
    $packages = $project.Project.ItemGroup.PackageReference.Include
    if ($packages) {
        $packages | ForEach-Object {
            $package = $PSItem
            Write-Host -Object "Updating $package in $projectName..." -ForegroundColor Green
            Invoke-Expression -Command "dotnet add '$path' package $package"
        }
    }
}
