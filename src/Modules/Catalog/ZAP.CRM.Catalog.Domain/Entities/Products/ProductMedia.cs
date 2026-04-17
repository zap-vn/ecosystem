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

namespace ZAP.CRM.Catalog.Domain.Entities.Products;
    [Table("product_media", Schema = "catalog")]
    public class ProductMedia
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();
        
        [Column("product_variant_id")]
        public Guid product_variant_id { get; set; }
        
        [Column("media_url")]
        public string media_url { get; set; } = string.Empty;
        
        [Column("is_primary")]
        public bool is_primary { get; set; } = false;
        
        [Column("sort_order")]
        public int sort_order { get; set; } = 0;

        // Navigation
        public ProductVariant? variant { get; set; }
    }



