using System;
using System.IO;
using System.Text.RegularExpressions;

foreach (var f in new[] { "\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Brands\v1\Commands\UpdateBrandCommandHandler.cs","\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Brands\v1\Queries\GetBrandByIdQueryHandler.cs","\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Management\v1\Queries\GetProductByIdQueryHandler.cs","\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Customers\v1\Commands\UpdateCustomerGroupCommandHandler.cs","\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Payments\v1\Commands\UpdatePaymentTermsCommandHandler.cs","\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Payments\v1\Commands\UpdatePaymentTypeCommandHandler.cs","\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Products\v1\Queries\GetProductByIdQueryHandler.cs","\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Sales\v1\Reports\Queries\GetOverviewListLocation\GetOverviewListLocationQueryHandler.cs","\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Promotions\v1\Promotions\Commands\UpdatePromotionCommandHandler.cs","\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Reports\v1\Reports\Commands\UpdateReportCommandHandler.cs" })
{
    var text = File.ReadAllText(f);
    int handleIdx = text.IndexOf("Handle(");
    if (handleIdx > -1) {
        int openBrace = text.IndexOf('{', handleIdx);
        if (openBrace > -1) {
            int depth = 1;
            int closeBrace = -1;
            for (int i = openBrace + 1; i < text.Length; i++) {
                if (text[i] == '{') depth++;
                if (text[i] == '}') depth--;
                if (depth == 0) {
                    closeBrace = i;
                    break;
                }
            }
            if (closeBrace > -1) {
                var newText = text.Substring(0, openBrace + 1) + "\n            throw new System.NotImplementedException();\n        " + text.Substring(closeBrace);
                File.WriteAllText(f, newText);
            }
        }
    }
}
