$files = @(
    'ManagementController.cs', 'CategoriesController.cs', 'LocationsController.cs', 
    'BrandsController.cs', 'MenusController.cs', 'CustomerGroupsController.cs', 
    'ModifierGroupsController.cs', 'CustomersController.cs', 'DiningOptionsController.cs', 
    'CollectionsController.cs', 'OrdersController.cs', 'PaymentTypesController.cs', 
    'ProductsController.cs', 'PaymentTermsController.cs', 'UomController.cs'
)
$dir = 'd:\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.API.CRM'
foreach ($f in (Get-ChildItem -Path $dir -Recurse -Filter *.cs)) {
    if ($files -contains $f.Name) {
        $content = Get-Content $f.FullName -Raw
        $content = $content -replace [char]0x200B, '' # remove Zero-Width Space if any
        # Let's also check `{` brace explicitly!
        # What if it's `{` followed by something weird?
        $content = $content -replace "namespace ZAP.Ecosystem.API.CRM", "namespace ZAP.Ecosystem.API.CRM"
        Set-Content -Path $f.FullName -Value $content -Encoding UTF8
    }
}
