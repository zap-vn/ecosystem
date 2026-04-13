param(
    [string]$ApiKey = "PMAK-69dc88876f6164000169b5f4-2203c9ca1e1cb79a445f2a53becdfc7a30",
    [string]$CollectionFile = "$PSScriptRoot\ZAP_Identity.postman_collection.json"
)

try {
    Write-Host "ZAP Postman Auto-Sync: Preparing to push collection to Postman Cloud..." -ForegroundColor Cyan

    # Force UTF8 for the environment
    $OutputEncoding = [System.Text.Encoding]::UTF8
    [Console]::OutputEncoding = [System.Text.Encoding]::UTF8

    # Load JSON content with explicit UTF8
    if (Test-Path $CollectionFile) {
        $jsonRaw = [System.IO.File]::ReadAllText($CollectionFile, [System.Text.Encoding]::UTF8)
        $collectionContent = $jsonRaw | ConvertFrom-Json
    } else {
        throw "Collection file not found at $CollectionFile"
    }
    
    $body = @{ collection = $collectionContent } | ConvertTo-Json -Depth 50 -Compress
    # Ensure body is treated as UTF8 bytes for the request
    $bodyBytes = [System.Text.Encoding]::UTF8.GetBytes($body)

    $headers = @{
        "X-Api-Key" = $ApiKey
        "Content-Type" = "application/json; charset=utf-8"
    }

    # Fetch all collections to see if we need to POST (create) or PUT (update)
    Write-Host "Fetching existing Workspaces/Collections..." -ForegroundColor Gray
    $allCollections = Invoke-RestMethod -Uri "https://api.getpostman.com/collections" -Method Get -Headers $headers
    $existingCol = $allCollections.collections | Where-Object { $_.name -eq $collectionContent.info.name }

    if ($existingCol) {
        Write-Host "Collection exists on Cloud. Updating UID: $($existingCol.uid)..." -ForegroundColor Yellow
        $response = Invoke-RestMethod -Uri "https://api.getpostman.com/collections/$($existingCol.uid)" -Method Put -Headers $headers -Body $bodyBytes
        Write-Host "Success! Collection seamlessly updated!" -ForegroundColor Green
    } else {
        Write-Host "Collection not found. Creating a new one on Cloud..." -ForegroundColor Yellow
        $response = Invoke-RestMethod -Uri "https://api.getpostman.com/collections" -Method Post -Headers $headers -Body $bodyBytes
        Write-Host "Success! New Collection officially minted!" -ForegroundColor Green
    }
    
    # Render final URL structure
    $uid = if ($existingCol) { $existingCol.uid } else { $response.collection.uid }
    Write-Host "--------------------------------------------------------" -ForegroundColor Cyan
    Write-Host "View your Collection at the Postman Web Workspace!" -ForegroundColor Cyan
    Write-Host "ID Output: $uid" -ForegroundColor Gray
} catch {
    Write-Error "Failed to push Postman collection! Exception: $_"
}
