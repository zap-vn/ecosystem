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
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZAP.CRM.Catalog.Domain.Entities.Commons;
    [Table("uom_item", Schema = "catalog")]
    public class UomItem
    {
        [Key]
        public int id { get; set; }
        public Guid tenant_id { get; set; }
        public string code { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;

        /// <summary>Short symbol displayed in UI, e.g. "Kg", "L", "Box".</summary>
        public string? abbreviation { get; set; }

        /// <summary>Number of decimal places allowed (0–5).</summary>
        public int precision { get; set; } = 0;

        public int? status_id { get; set; }

        // Navigation
        [ForeignKey("status_id")]
        public StatusItem? status { get; set; }
        public ICollection<UomItemTranslation> translations { get; set; } = new List<UomItemTranslation>();
    }

    [Table("uom_item", Schema = "catalog")]
    public class UomItemTranslation
    {
        [Key]
        public Guid id { get; set; }
        public int uom_item_id { get; set; }
        public int locale_id { get; set; }
        public string? name { get; set; }
    }



