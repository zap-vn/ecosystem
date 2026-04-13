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

    $targetApiFeatureDir = "$TargetApiProj\Features\$pluralName\v1\Controllers"
    $targetAppFeatureDir = "$TargetAppProj\Features\$pluralName\v1"

    New-Item -ItemType Directory -Force -Path $targetApiFeatureDir | Out-Null
    New-Item -ItemType Directory -Force -Path $targetAppFeatureDir\Commands | Out-Null
    New-Item -ItemType Directory -Force -Path $targetAppFeatureDir\Queries | Out-Null
    New-Item -ItemType Directory -Force -Path $targetAppFeatureDir\DTOs | Out-Null

    # Migrate Controllers
    $apiControllers = Get-ChildItem -Path "$servicePath\*.Api\Controllers" -Filter "*.cs" -Recurse -ErrorAction SilentlyContinue
    foreach ($ctrl in $apiControllers) {
        $content = Get-Content $ctrl.FullName -Raw
        $content = $content -replace 'namespace CRM\.[^\.]+\.Api\.Controllers.*', "namespace ZAP.Ecosystem.API.CRM.Features.$pluralName.v1.Controllers`r`n{"
        $content = $content -replace 'using CRM\.[^\.]+\.Application\.', 'using ZAP.Ecosystem.Application.CRM.Features.'
        
        $destPath = "$targetApiFeatureDir\$($ctrl.Name)"
        Set-Content -Path $destPath -Value $content -Encoding UTF8
        Write-Host "Migrated Controller: $($ctrl.Name)"
    }

    # Migrate Application Commands
    $appCommands = Get-ChildItem -Path "$servicePath\*.Application\Commands" -Filter "*.cs" -Recurse -ErrorAction SilentlyContinue
    foreach ($cmd in $appCommands) {
        $content = Get-Content $cmd.FullName -Raw
        $content = $content -replace 'namespace CRM\.[^\.]+\.Application\.Commands.*', "namespace ZAP.Ecosystem.Application.CRM.Features.$pluralName.v1.Commands`r`n{"
        $destPath = "$targetAppFeatureDir\Commands\$($cmd.Name)"
        Set-Content -Path $destPath -Value $content -Encoding UTF8
        Write-Host "Migrated Command: $($cmd.Name)"
    }

    # Migrate Application Queries
    $appQueries = Get-ChildItem -Path "$servicePath\*.Application\Queries" -Filter "*.cs" -Recurse -ErrorAction SilentlyContinue
    foreach ($qry in $appQueries) {
        $content = Get-Content $qry.FullName -Raw
        $content = $content -replace 'namespace CRM\.[^\.]+\.Application\.Queries.*', "namespace ZAP.Ecosystem.Application.CRM.Features.$pluralName.v1.Queries`r`n{"
        $destPath = "$targetAppFeatureDir\Queries\$($qry.Name)"
        Set-Content -Path $destPath -Value $content -Encoding UTF8
        Write-Host "Migrated Query: $($qry.Name)"
    }
}
Write-Host "Migration completed successfully!"
