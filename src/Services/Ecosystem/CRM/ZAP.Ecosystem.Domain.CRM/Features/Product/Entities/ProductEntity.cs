using ZAP.Ecosystem.Domain.CRM.Common;
using System.Collections.Generic;

namespace CRM.Product.Domain.Entities
{
        public class ProductEntity : BaseEntity, ILocalizable<ProductTranslation>
    {
                public override string? UserGuid { get; set; }

                public string? EmpGuid { get; set; }

                public string Code { get; set; } = string.Empty;

                public string Barcode { get; set; } = string.Empty;

                public string Name { get; set; } = string.Empty; 

                public string Description { get; set; } = string.Empty;

                        public decimal Price { get; set; }

                public string Category { get; set; } = string.Empty;

                public string ImageUrl { get; set; } = string.Empty;

                public int Stock { get; set; }

                public int Visible { get; set; } = 1;

                public bool IsActive => Visible == 1;
        
        public ICollection<ProductTranslation> Translations { get; set; } = new List<ProductTranslation>();
    }
}
