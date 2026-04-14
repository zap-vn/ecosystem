$files = @('CategoryCategoryMap.cs', 'ModifierGroupCategoryMap.cs', 'UnitCategoryMap.cs', 'CategoryEntity.cs', 'ModifierGroupEntity.cs', 'UnitEntity.cs', 'CategoryLocationPricing.cs', 'ModifierGroupLocationPricing.cs', 'UnitLocationPricing.cs', 'CategoryMedia.cs', 'ModifierGroupMedia.cs', 'UnitMedia.cs', 'CategoryTranslation.cs', 'ModifierGroupTranslation.cs', 'UnitTranslation.cs', 'CategoryTypeItem.cs', 'ModifierGroupTypeItem.cs', 'UnitTypeItem.cs', 'CategoryVariant.cs', 'ModifierGroupVariant.cs', 'UnitVariant.cs', 'Unit.cs')
foreach ($f in $files) {
    $path = "d:\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Domain.CRM\Entities\$f"
    if (Test-Path $path) {
        Remove-Item -Path $path -Force
        Write-Host "Deleted $f"
    }
}
