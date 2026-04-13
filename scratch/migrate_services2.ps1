param (
    [string[]]$SourceServices,
    [string]$TargetApiProj,
    [string]$TargetAppProj
)

foreach ($servicePath in $SourceServices) {
    if (-not (Test-Path $servicePath)) {
        Write-Host "Path not found $servicePath"
        continue
    }
    
    $serviceName = (Get-Item $servicePath).Name
    $pluralName = $serviceName
    if ($serviceName -eq "Category") { $pluralName = "Categories" }
    elseif ($serviceName -eq "Brand") { $pluralName = "Brands" }
    elseif ($serviceName -eq "Collection") { $pluralName = "Collections" }
    elseif ($serviceName -eq "Customer") { $pluralName = "Customers" }
    elseif ($serviceName -eq "Product") { $pluralName = "Products" }
    elseif ($serviceName -eq "Promotion") { $pluralName = "Promotions" }
    elseif ($serviceName -eq "DiningOption") { $pluralName = "DiningOptions" }
    elseif ($serviceName -eq "Location") { $pluralName = "Locations" }
    elseif ($serviceName -eq "Order") { $pluralName = "Orders" }
    elseif ($serviceName -eq "Payment") { $pluralName = "Payments" }
    elseif ($serviceName -eq "Unit") { $pluralName = "Units" }

    $targetAppFeatureDir = "$TargetAppProj\Features\$pluralName\v1"

    # Migrate Application Commands
    $appCommands = Get-ChildItem -Path "$servicePath\*.Application\Features" -Filter "*.cs" -Recurse -ErrorAction SilentlyContinue | Where-Object { $_.DirectoryName -match "Commands" }
    foreach ($cmd in $appCommands) {
        $content = Get-Content $cmd.FullName -Raw
        $content = $content -replace 'namespace CRM\.[^\.]+\.Application\.Features\.[^\.]+\.Commands', "namespace ZAP.Ecosystem.Application.CRM.Features.$pluralName.v1.Commands"
        $destPath = "$targetAppFeatureDir\Commands\$($cmd.Name)"
        Set-Content -Path $destPath -Value $content -Encoding UTF8
        Write-Host "Migrated Command: $($cmd.Name)"
    }

    # Migrate Application Queries
    $appQueries = Get-ChildItem -Path "$servicePath\*.Application\Features" -Filter "*.cs" -Recurse -ErrorAction SilentlyContinue | Where-Object { $_.DirectoryName -match "Queries" }
    foreach ($qry in $appQueries) {
        $content = Get-Content $qry.FullName -Raw
        $content = $content -replace 'namespace CRM\.[^\.]+\.Application\.Features\.[^\.]+\.Queries', "namespace ZAP.Ecosystem.Application.CRM.Features.$pluralName.v1.Queries"
        $destPath = "$targetAppFeatureDir\Queries\$($qry.Name)"
        Set-Content -Path $destPath -Value $content -Encoding UTF8
        Write-Host "Migrated Query: $($qry.Name)"
    }
    
    # Migrate Application DTOs
    $appDTOs = Get-ChildItem -Path "$servicePath\*.Application\Features" -Filter "*.cs" -Recurse -ErrorAction SilentlyContinue | Where-Object { $_.DirectoryName -match "DTOs" }
    foreach ($dto in $appDTOs) {
        $content = Get-Content $dto.FullName -Raw
        $content = $content -replace 'namespace CRM\.[^\.]+\.Application\.Features\.[^\.]+\.DTOs', "namespace ZAP.Ecosystem.Application.CRM.Features.$pluralName.v1.DTOs"
        $destPath = "$targetAppFeatureDir\DTOs\$($dto.Name)"
        Set-Content -Path $destPath -Value $content -Encoding UTF8
        Write-Host "Migrated DTO: $($dto.Name)"
    }
}
Write-Host "Migration completed successfully!"
