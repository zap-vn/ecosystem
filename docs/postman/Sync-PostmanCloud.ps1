param(
    [string]$ApiKey = "PMAK-69dc88876f6164000169b5f4-2203c9ca1e1cb79a445f2a53becdfc7a30",
    [string]$CollectionFile = "$PSScriptRoot\ZAP_Identity.postman_collection.json"
)

# Essential: Set UTF-8 encoding for the entire PowerShell session
$OutputEncoding = [System.Text.Encoding]::UTF8
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

try {
    Write-Host "ZAP Postman Auto-Sync: Preparing to push collection to Postman Cloud (Strict Byte Mode)..." -ForegroundColor Cyan

    if (-not (Test-Path $CollectionFile)) {
        throw "Collection file not found at $CollectionFile"
    }

    # 1. Read Raw Bytes directly from the file to avoid any string mangling
    $rawBytes = [System.IO.File]::ReadAllBytes($CollectionFile)
    
    # 2. To get the collection name for matching, we parse it once but don't use it for the push
    $jsonString = [System.Text.Encoding]::UTF8.GetString($rawBytes)
    $collectionObj = $jsonString | ConvertFrom-Json
    $collectionName = $collectionObj.info.name

    # 3. Prepare the Wrapper Body as Bytes
    # Postman API expects { "collection": { ... } }
    $wrapperStart = [System.Text.Encoding]::UTF8.GetBytes('{"collection":')
    $wrapperEnd = [System.Text.Encoding]::UTF8.GetBytes('}')
    
    # Combine bytes: Start + RawFileBytes + End
    $fullBodyBytes = New-Object byte[] ($wrapperStart.Length + $rawBytes.Length + $wrapperEnd.Length)
    [System.Buffer]::BlockCopy($wrapperStart, 0, $fullBodyBytes, 0, $wrapperStart.Length)
    [System.Buffer]::BlockCopy($rawBytes, 0, $fullBodyBytes, $wrapperStart.Length, $rawBytes.Length)
    [System.Buffer]::BlockCopy($wrapperEnd, 0, $fullBodyBytes, ($wrapperStart.Length + $rawBytes.Length), $wrapperEnd.Length)

    $headers = @{
        "X-Api-Key" = $ApiKey
        "Content-Type" = "application/json; charset=utf-8"
    }

    Write-Host "Syncing with Cloud API..." -ForegroundColor Gray
    $allCollections = Invoke-RestMethod -Uri "https://api.getpostman.com/collections" -Method Get -Headers $headers
    $existingCol = $allCollections.collections | Where-Object { $_.name -eq $collectionName }

    if ($existingCol) {
        Write-Host "Updating existing Collection: $($existingCol.uid)" -ForegroundColor Yellow
        $response = Invoke-RestMethod -Uri "https://api.getpostman.com/collections/$($existingCol.uid)" -Method Put -Headers $headers -Body $fullBodyBytes
        Write-Host "SUCCESS: UTF-8 Binary Sync Completed!" -ForegroundColor Green
    } else {
        Write-Host "Creating new Cloud collection..." -ForegroundColor Yellow
        $response = Invoke-RestMethod -Uri "https://api.getpostman.com/collections" -Method Post -Headers $headers -Body $fullBodyBytes
        Write-Host "SUCCESS: New Collection minted!" -ForegroundColor Green
    }
} catch {
    Write-Error "UTF-8 Sync Failed! Exception: $_"
}
