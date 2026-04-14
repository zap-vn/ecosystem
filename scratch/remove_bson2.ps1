$files = Get-ChildItem -Path d:\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Domain.CRM -Recurse -Filter *.cs
foreach ($f in $files) {
    if ($f.FullName -match "\\obj\\" -or $f.FullName -match "\\bin\\") { continue }
    $c = Get-Content $f.FullName -Raw
    $c = $c -replace '\[Bson[^\]]*\]\s*', ''
    # Replace IMongoCollection as well
    $c = $c -replace 'IMongoCollection<', 'DbSet<'
    Set-Content -Path $f.FullName -Value $c -NoNewline
}
