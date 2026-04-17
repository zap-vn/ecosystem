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

namespace ZAP.CRM.Catalog.Domain.Entities.Brands;
    [Table("brand", Schema = "catalog")]
    public class Brand
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();

        [Column("serial_id")]
        public int? serial_id { get; set; }

        public Guid? tenant_id { get; set; }
        public string name { get; set; } = string.Empty;
        public string slug { get; set; } = string.Empty;
        public string logo_url { get; set; } = string.Empty;
        public string banner_url { get; set; } = string.Empty;
        public string website_url { get; set; } = string.Empty;
        public string? brand_code { get; set; }
        public int status_id { get; set; } = 2101;
        public bool is_premium { get; set; } = false;
        public bool is_active { get; set; } = true;

        [ForeignKey("status_id")]
        public StatusItem? status { get; set; }
    }



