$files = Get-ChildItem -Path "src\Services\Ecosystem\CRM\ZAP.Ecosystem.API.CRM\Features" -Filter "*.cs" -Recurse
foreach ($file in $files) {
    $content = Get-Content -Path $file.FullName -Raw
    if ($content -match "\{\r?\n\{") {
        $content = $content -replace "\{\r?\n\{", "{"
        Set-Content -Path $file.FullName -Value $content
        Write-Host "Fixed $($file.FullName)"
    }
}
