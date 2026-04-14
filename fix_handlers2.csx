using System;
using System.IO;

foreach (var f in new[] { "\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Reports\v1\Reports\Queries\GetReportListQueryHandler.cs","\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Customers\v1\Commands\CreateCustomerCommandHandler.cs","\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Payments\v1\Queries\GetPaymentTypeListQueryHandler.cs","\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Customers\v1\Queries\GetCustomerGroupListQueryHandler.cs","\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Products\v1\Commands\CreateModifierGroupCommandHandler.cs","\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Payments\v1\Queries\GetPaymentTermsListQueryHandler.cs","\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Customers\v1\Queries\GetCustomerListQueryHandler.cs","\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Products\v1\Queries\GetBrandsQueryHandler.cs","\PROJECTS\4_CRM_API\src\Services\Ecosystem\CRM\ZAP.Ecosystem.Application.CRM\Features\Products\v1\Queries\GetProductListQueryHandler.cs" })
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
