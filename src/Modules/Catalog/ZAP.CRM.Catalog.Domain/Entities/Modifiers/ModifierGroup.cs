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

namespace ZAP.CRM.Catalog.Domain.Entities.Modifiers;
    [Table("modifier_group", Schema = "catalog")]
    public class ModifierGroup
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        public Guid tenant_id { get; set; }
        
        [Column("serial_id")]
        public int? serial_id { get; set; }

        [Column("serial_number")]
        public string? serial_number { get; set; }
        
        public string? legacy_id { get; set; }
        public string name { get; set; } = string.Empty;
        public string? description { get; set; }
        public string? image_url { get; set; }
        public int min_selection { get; set; } = 0;
        public int max_selection { get; set; } = 1;
        public bool is_required { get; set; } = false;
        public int sort_order { get; set; } = 0;
        
        [Column("status_id")]
        public int? status_id { get; set; }
        
        [ForeignKey("status_id")]
        public StatusItem? status { get; set; }

        [NotMapped]
        public string? status_code { get; set; }

        [NotMapped]
        public string? status_name { get; set; }
    }



