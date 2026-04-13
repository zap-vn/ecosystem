$dir = "d:\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.API.CRM\Features"
Get-ChildItem -Path $dir -Filter "*.cs" -Recurse | ForEach-Object {
    $content = Get-Content $_.FullName -Raw
    $newContent = $content -replace 'using ZAP\.Ecosystem\.Application\.CRM\.Features\.Features\.([a-zA-Z0-9_]+)\.(Commands|DTOs|Queries);', 'using ZAP.Ecosystem.Application.CRM.Features.$1.v1.$2;'
    $newContent = $newContent -replace 'using CRM\.BuildingBlocks[^;]*;', 'using ZAP.Ecosystem.Shared.Data;'
    [System.IO.File]::WriteAllText($_.FullName, $newContent)
}
