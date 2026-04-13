using ZAP.Ecosystem.Domain.CRM.Common;

namespace CRM.Sales.Domain.Entities.Products
{
    public class ProductTranslation : BaseTranslationEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
