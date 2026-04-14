$domainDir = "d:\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Domain.CRM"
$files = Get-ChildItem -Path $domainDir -Recurse -Filter *.cs

foreach ($file in $files) {
    if ($file.FullName -match "\\obj\\" -or $file.FullName -match "\\bin\\") { continue }
    $content = Get-Content $file.FullName -Raw

    $original = $content

    # Remove MongoDB usings
    $content = $content -replace '(?m)^using MongoDB.*?;\r?\n', ''

    # Replace CRM.BuildingBlocks with Common
    $content = $content -replace '(?m)^using CRM\.BuildingBlocks.*?;\r?\n', ''
    
    # It might be safe to just insert 'using ZAP.Ecosystem.Domain.CRM.Common;' at the top
    if ($content -notmatch 'using ZAP\.Ecosystem\.Domain\.CRM\.Common;') {
        $content = "using ZAP.Ecosystem.Domain.CRM.Common;`n" + $content
    }

    # Remove all Bson attributes
    $content = $content -replace '\[Bson[A-Za-z]+(\([^\)]*\))?\][ \t]*\r?\n?', ''

    # Replace types
    $content = $content -replace '\bIMongoRepository<', 'IRepository<'
    $content = $content -replace '\bObjectId\b', 'Guid'

    if ($original -ne $content) {
        Set-Content -Path $file.FullName -Value $content -NoNewline
    }
}
Write-Host "MongoDB removal completed."
