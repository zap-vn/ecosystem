using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        var files = new[] {
            @"D:\01-Working\PROJECTS\ZAP\Product\ecosystem\src\Modules\Catalog\ZAP.CRM.Catalog.Domain\Entities\Products\ProductCategoryMap.cs",
            @"D:\01-Working\PROJECTS\ZAP\Product\ecosystem\src\Modules\Catalog\ZAP.CRM.Catalog.Domain\Entities\Products\ProductLocationPricing.cs",
            @"D:\01-Working\PROJECTS\ZAP\Product\ecosystem\src\Modules\Catalog\ZAP.CRM.Catalog.Domain\Entities\Products\ProductTranslation.cs",
            @"D:\01-Working\PROJECTS\ZAP\Product\ecosystem\src\Modules\Finance\ZAP.Ecosystem.Finance.Domain\Entities\PaymentMethodTranslation.cs",
            @"D:\01-Working\PROJECTS\ZAP\Product\ecosystem\src\Modules\Finance\ZAP.Ecosystem.Finance.Domain\Entities\PaymentTermsTranslate.cs",
            @"D:\01-Working\PROJECTS\ZAP\Product\ecosystem\src\Modules\Finance\ZAP.Ecosystem.Finance.Domain\Entities\TranslatePaymentType.cs",
            @"D:\01-Working\PROJECTS\ZAP\Product\ecosystem\src\Modules\Finance\ZAP.Ecosystem.Finance.Domain\Entities\Reports\ReportTemplateTranslation.cs",
            @"D:\01-Working\PROJECTS\ZAP\Product\ecosystem\src\Modules\HRM\ZAP.Ecosystem.HRM.Domain\Entities\EmployeeTranslation.cs",
            @"D:\01-Working\PROJECTS\ZAP\Product\ecosystem\src\Modules\HRM\ZAP.Ecosystem.HRM.Domain\Entities\OrganizationUnitTranslation.cs",
            @"D:\01-Working\PROJECTS\ZAP\Product\ecosystem\src\Modules\Sales\ZAP.Ecosystem.Sales.Domain\Entities\OrderDetailEntity.cs",
            @"D:\01-Working\PROJECTS\ZAP\Product\ecosystem\src\Modules\Catalog\ZAP.CRM.Catalog.Domain\Entities\Locations\LocationEntity.cs"
        };

        foreach (var f in files)
        {
            if (!File.Exists(f)) continue;
            var c = File.ReadAllText(f);
            
            // Skip if it already has PrimaryKey or Key
            if (c.Contains("[PrimaryKey") || c.Contains("[Key]")) continue;
            
            // Make sure using Microsoft.EntityFrameworkCore; is there
            if (!c.Contains("using Microsoft.EntityFrameworkCore;"))
            {
                c = "using Microsoft.EntityFrameworkCore;\n" + c;
            }

            if (f.EndsWith("ProductCategoryMap.cs"))
                c = Regex.Replace(c, @"public class ProductCategoryMap", "[PrimaryKey(nameof(product_id), nameof(category_id))]\n    public class ProductCategoryMap");
            else if (f.EndsWith("ProductLocationPricing.cs"))
                c = Regex.Replace(c, @"public class ProductLocationPricing", "[PrimaryKey(nameof(product_variant_id), nameof(location_id))]\n    public class ProductLocationPricing");
            else if (c.Contains("public Guid id ") || c.Contains("public Guid id {"))
                c = Regex.Replace(c, @"(public Guid id \{)", "[Key]\n        ");
            else if (f.Contains("Translation") || f.Contains("Translate"))
            {
                // Most standard translations in ZAP Ecosystem inherit ITranslation
                // which have entity_id and language_code
                var name = Path.GetFileNameWithoutExtension(f);
                c = Regex.Replace(c, $@"public class {name}", $"[PrimaryKey(\"entity_id\", \"language_code\")]\n    public class {name}");
            }
            else if (f.EndsWith("OrderDetailEntity.cs"))
                c = Regex.Replace(c, @"public class OrderDetailEntity", "[PrimaryKey(\"order_id\", \"product_id\")]\n    public class OrderDetailEntity");
            else if (f.EndsWith("LocationEntity.cs"))
                c = Regex.Replace(c, @"public class LocationEntity", "[Key]\n    public class LocationEntity");

            File.WriteAllText(f, c);
            Console.WriteLine($"Patched {Path.GetFileName(f)}");
        }
    }
}
