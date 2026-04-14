$errorActionPreference = "Stop"
$root = "d:\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM"

Write-Host "Fixing EVERYTHING..."
Get-ChildItem -Path $root -Filter *.cs -Recurse | ForEach-Object {
    $content = Get-Content $_.FullName -Raw
    $newContent = $content

    # Fix inline domain references
    $newContent = $newContent -replace 'CRM\.[A-Za-z0-9_]+\.Domain\.Entities\.', ''
    $newContent = $newContent -replace 'CRM\.[A-Za-z0-9_]+\.Domain\.Interfaces\.', ''
    $newContent = $newContent -replace 'CRM\.[A-Za-z0-9_]+\.Domain\.', ''

    # Fix inline application references
    $newContent = $newContent -replace 'CRM\.[A-Za-z0-9_]+\.Application\.Features\.[A-Za-z0-9_]+\.DTOs\.', ''
    
    # Fix using legacy namespaces
    $newContent = $newContent -replace '(?m)^\s*using\s+CRM\.[A-Za-z0-9_]+\.Domain\..*;\r?\n?', ''
    $newContent = $newContent -replace '(?m)^\s*using\s+CRM\.[A-Za-z0-9_]+\.Application\..*;\r?\n?', ''
    $newContent = $newContent -replace '(?m)^\s*using\s+CRM\.BuildingBlocks\..*;\r?\n?', ''
    $newContent = $newContent -replace '(?m)^\s*using\s+CRM\.[A-Za-z0-9_]+\.Application\.Common\..*;\r?\n?', ''
    $newContent = $newContent -replace '(?m)^\s*using\s+CRM\.[A-Za-z0-9_]+\.Domain;\r?\n?', ''
    $newContent = $newContent -replace '(?m)^\s*using\s+CRM\.[A-Za-z0-9_]+;\r?\n?', ''
    
    # Replace application namespaces
    $newContent = $newContent -replace '(?m)^namespace\s+CRM\.[A-Za-z0-9_]+\.Application.*', 'namespace ZAP.Ecosystem.Application.CRM'

    # Any leftover CRM.BuildingBlocks or CRM.whatever inline? Let's just remove CRM.[Module].Application
    $newContent = $newContent -replace 'CRM\.[A-Za-z0-9_]+\.Application\.', ''

    if ($newContent -cne $content) {
        Set-Content -Path $_.FullName -Value $newContent -Encoding UTF8
    }
}
Write-Host "Done!"
