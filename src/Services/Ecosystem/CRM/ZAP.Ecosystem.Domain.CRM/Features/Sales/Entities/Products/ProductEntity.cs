using ZAP.Ecosystem.Domain.CRM.Common;
using System.Collections.Generic;

namespace CRM.Sales.Domain.Entities.Products
{
    public class ProductEntity : BaseEntity, ILocalizable<ProductTranslation>
    {
                public override string? UserGuid { get; set; }

        public string Code { get; set; } = string.Empty;
        
        // These can act as default values (vi-VN)
        public string Name { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty;
        
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Stock { get; set; }
        public bool IsActive { get; set; } = true;
        
        public ICollection<ProductTranslation> Translations { get; set; } = new List<ProductTranslation>();
    }
}
