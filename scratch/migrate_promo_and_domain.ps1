$legacyDir = "D:\PROJECTS\4_2026\01042026\Services"
$ecoCrmDir = "D:\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM"
$ecoDomainDir = "$ecoCrmDir\ZAP.Ecosystem.Domain.CRM"

# Modules that need API & Application migration
$modulesToMigrate = @("Promotion", "Organization", "Report", "Sales", "HR")

Write-Host "Migrating API and Application for missing modules..."
foreach ($mod in $modulesToMigrate) {
    $pluralMod = $mod + "s"
    if ($mod -match "s$") { $pluralMod = $mod } # Sales -> Sales

    # Source folders
    $srcApi = "$legacyDir\$mod\CRM.$mod.Api\Controllers"
    $srcApp = "$legacyDir\$mod\CRM.$mod.Application\Features"

    # Dest folders
    $destApi = "$ecoCrmDir\ZAP.Ecosystem.API.CRM\Features\$pluralMod\v1\Controllers"
    $destApp = "$ecoCrmDir\ZAP.Ecosystem.Application.CRM\Features\$pluralMod\v1"

    if (Test-Path $srcApi) {
        New-Item -ItemType Directory -Force -Path $destApi | Out-Null
        Copy-Item -Path "$srcApi\*" -Destination $destApi -Recurse -Force
    }
    
    if (Test-Path $srcApp) {
        New-Item -ItemType Directory -Force -Path $destApp | Out-Null
        Copy-Item -Path "$srcApp\*" -Destination $destApp -Recurse -Force
    }
}

Write-Host "Creating ZAP.Ecosystem.Domain.CRM project..."
if (-not (Test-Path $ecoDomainDir)) {
    New-Item -ItemType Directory -Force -Path $ecoDomainDir | Out-Null
    $csprojContent = @"
<Project Sdk=`"Microsoft.NET.Sdk`">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <ProjectReference Include=`"..\..\ZAP.Ecosystem.Shared\ZAP.Ecosystem.Shared.csproj`" />
  </ItemGroup>
</Project>
"@
    Set-Content -Path "$ecoDomainDir\ZAP.Ecosystem.Domain.CRM.csproj" -Value $csprojContent
}

# All modules to extract Domain from
$allModules = @("Brand", "Category", "Collection", "Customer", "DiningOption", "Location", "Management", "Menu", "ModifierGroup", "Order", "Payment", "Product", "Promotion", "Unit", "Organization", "Report", "Sales", "HR")

Write-Host "Migrating Domain Entities and Interfaces..."
foreach ($mod in $allModules) {
    $srcDomain = "$legacyDir\$mod\CRM.$mod.Domain"
    $destDomain = "$ecoDomainDir\Features\$mod"

    if (Test-Path $srcDomain) {
        New-Item -ItemType Directory -Force -Path $destDomain | Out-Null
        if (Test-Path "$srcDomain\Entities") {
            Copy-Item -Path "$srcDomain\Entities" -Destination $destDomain -Recurse -Force
        }
        if (Test-Path "$srcDomain\Interfaces") {
            Copy-Item -Path "$srcDomain\Interfaces" -Destination $destDomain -Recurse -Force
        }
    }
}

Write-Host "Adding Domain reference to Application..."
dotnet add "$ecoCrmDir\ZAP.Ecosystem.Application.CRM\ZAP.Ecosystem.Application.CRM.csproj" reference "$ecoDomainDir\ZAP.Ecosystem.Domain.CRM.csproj"

Write-Host "Migration script completed."
