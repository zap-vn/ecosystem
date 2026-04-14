$domainRoot = "d:\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Domain.CRM"
$featuresRoot = "$domainRoot\Features"
$entitiesRoot = "$domainRoot\Entities"
$interfacesRoot = "$domainRoot\Interfaces"

if (!(Test-Path $entitiesRoot)) { New-Item -ItemType Directory -Path $entitiesRoot | Out-Null }
if (!(Test-Path $interfacesRoot)) { New-Item -ItemType Directory -Path $interfacesRoot | Out-Null }

$fileDict = @{}

Get-ChildItem -Path $featuresRoot -Filter *.cs -Recurse | ForEach-Object {
    $file = $_
    $destFolder = ""
    if ($file.DirectoryName -match "Entities") {
        $destFolder = $entitiesRoot
    } elseif ($file.DirectoryName -match "Interfaces") {
        $destFolder = $interfacesRoot
    } else {
        $destFolder = "$domainRoot\Other"
        if (!(Test-Path $destFolder)) { New-Item -ItemType Directory -Path $destFolder | Out-Null }
    }

    if (-not $fileDict.ContainsKey($file.Name)) {
        $fileDict[$file.Name] = $file
    } else {
        if ($file.Length -gt $fileDict[$file.Name].Length) {
            $fileDict[$file.Name] = $file
        }
    }
}

Write-Host "Found $($fileDict.Count) unique files. Consolidating..."

foreach ($key in $fileDict.Keys) {
    Set-Variable -Name file -Value $fileDict[$key]
    $destFolder = ""
    if ($file.DirectoryName -match "Entities") {
        $destFolder = $entitiesRoot
    } elseif ($file.DirectoryName -match "Interfaces") {
        $destFolder = $interfacesRoot
    } else {
        $destFolder = "$domainRoot\Other"
    }
    $destPath = Join-Path $destFolder $file.Name
    Copy-Item -Path $file.FullName -Destination $destPath -Force
}

Remove-Item -Path $featuresRoot -Recurse -Force
Write-Host "Done!"
