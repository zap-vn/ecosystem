$dir = "d:\Working\01-Project\COMPANY\ZAP\Project\ecosystem\v100\ecosystem\src"
$files = Get-ChildItem -Path $dir -Recurse -File | Select-String -Pattern "<<<<<<< HEAD" -List | Select-Object -ExpandProperty Path

foreach ($file in $files) {
    Write-Host "Resolving: $file"
    $content = Get-Content $file -Raw
    
    if ($file -match "Program\.cs" -or $file -match "\.csproj") {
        # Keep HEAD
        $content = [regex]::Replace($content, '(?s)<<<<<<< HEAD\r?\n(.*?)\r?\n=======\r?\n.*?\r?\n>>>>>>>[^\r\n]*\r?\n?', '$1')
    } else {
        # Keep DEV
        $content = [regex]::Replace($content, '(?s)<<<<<<< HEAD\r?\n.*?\r?\n=======\r?\n(.*?)\r?\n>>>>>>>[^\r\n]*\r?\n?', '$1')
    }
    
    Set-Content -Path $file -Value $content
}
Write-Host "Done resolving!"
