$errorActionPreference = "Stop"
$apiRoot = "d:\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM"

# 1. Fix Domain project
$domainRoot = "$apiRoot\ZAP.Ecosystem.Domain.CRM"
Write-Host "Fixing Domain namespaces..."
Get-ChildItem -Path $domainRoot -Filter *.cs -Recurse | ForEach-Object {
    $content = Get-Content $_.FullName -Raw
    $newContent = $content

    # Remove intra-domain usings
    $newContent = $newContent -replace '(?m)^\s*using\s+CRM\.[A-Za-z0-9_]+\.Domain\..*;\r?\n?', ''
    
    # Replace the legacy namespaces with ZAP.Ecosystem.Domain.CRM
    $newContent = $newContent -replace '(?m)^namespace\s+CRM\.[A-Za-z0-9_]+\.Domain\.[A-Za-z0-9_]+', 'namespace ZAP.Ecosystem.Domain.CRM'

    if ($newContent -cne $content) {
        Set-Content -Path $_.FullName -Value $newContent -Encoding UTF8
    }
}

# 2. Fix Application project
$appRoot = "$apiRoot\ZAP.Ecosystem.Application.CRM"
Write-Host "Fixing Application namespaces..."
Get-ChildItem -Path $appRoot -Filter *.cs -Recurse | ForEach-Object {
    $content = Get-Content $_.FullName -Raw
    $newContent = $content

    # Remove legacy domain usings
    $newContent = $newContent -replace '(?m)^\s*using\s+CRM\.[A-Za-z0-9_]+\.Domain\..*;\r?\n?', ''
    $newContent = $newContent -replace '(?m)^\s*using\s+ZAP\.Ecosystem\.Domain\.CRM\.Features\..*;\r?\n?', ''
    
    # Remove old application usings that are broken
    $newContent = $newContent -replace '(?m)^\s*using\s+CRM\.[A-Za-z0-9_]+\.Application\..*;\r?\n?', ''

    # Ensure using ZAP.Ecosystem.Domain.CRM is present
    if ($newContent -notmatch 'using ZAP\.Ecosystem\.Domain\.CRM;') {
        $newContent = "using ZAP.Ecosystem.Domain.CRM;`r`n" + $newContent
    }

    # Fix inline full qualifications (e.g. CRM.Product.Domain.Entities.Product -> Product)
    $newContent = $newContent -replace 'CRM\.[A-Za-z0-9_]+\.Domain\.Entities\.', ''
    $newContent = $newContent -replace 'CRM\.[A-Za-z0-9_]+\.Domain\.Interfaces\.', ''

    # We need to make sure DTOs and Queries within the same feature can see each other,
    # but some files need DTOs from other features.
    # To fix "The type or namespace name 'ProductDto' could not be found", 
    # let's just add a few global using equivalents for the DTO folders at the top, or specific usings.
    # Let's add all DTO namespaces to ALL files to be safe, since they might share DTOs.
    $dtoUsings = @"
using ZAP.Ecosystem.Application.CRM.Features.Products.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.Customers.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.Brands.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.Categories.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.DiningOptions.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.Locations.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.Management.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.Menu.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.Promotions.v1.Promotions.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.Units.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.Sales.v1.Reports.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.Payments.v1.DTOs;
"@
    
    # Only insert DTO usings if it's a Command/Query/Handler that might need them
    if ($newContent -match 'IRequestHandler' -or $newContent -match 'IRequest') {
        # Check if they are already present to avoid duplication
        if ($newContent -notmatch 'using ZAP\.Ecosystem\.Application\.CRM\.Features\.Products\.v1\.DTOs;') {
            $newContent = $dtoUsings + "`r`n" + $newContent
        }
    }

    if ($newContent -cne $content) {
        Set-Content -Path $_.FullName -Value $newContent -Encoding UTF8
    }
}

Write-Host "Done!"
