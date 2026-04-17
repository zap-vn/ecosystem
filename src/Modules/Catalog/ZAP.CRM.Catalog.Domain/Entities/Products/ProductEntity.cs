using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZAP.CRM.Catalog.Domain.Interfaces.Brands;
using ZAP.CRM.Catalog.Domain.Interfaces.Products;
using ZAP.CRM.Catalog.Domain.Interfaces.Menus;
using ZAP.CRM.Catalog.Domain.Interfaces.Categories;
using ZAP.CRM.Catalog.Domain.Interfaces.Locations;
using ZAP.CRM.Catalog.Domain.Interfaces.Modifiers;
using ZAP.CRM.Catalog.Domain.Interfaces.Geography;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Menus;
using ZAP.CRM.Catalog.Domain.Entities.Categories;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using ZAP.CRM.Catalog.Domain.Entities.Modifiers;
using ZAP.CRM.Catalog.Domain.Entities.Geography;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using ZAP.CRM.Catalog.Domain.Interfaces;
using ZAP.Ecosystem.Shared.Entities;
using System.Collections.Generic;

namespace ZAP.CRM.Catalog.Domain.Entities.Products;
        [Table("product", Schema = "catalog")]
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



