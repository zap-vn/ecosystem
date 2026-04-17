using System.ComponentModel.DataAnnotations;
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
using System.ComponentModel.DataAnnotations.Schema;

namespace ZAP.CRM.Catalog.Domain.Entities.Products;
    [Table("product_variant", Schema = "catalog")]
    public class ProductVariant
    {
        public Guid id { get; set; } = Guid.NewGuid();

        [Column("serial_id")]
        public int? serial_id { get; set; }

        public Guid? product_id { get; set; }
        public string? sku_code { get; set; } // Mã SKU
        public string? barcode { get; set; }
        public string? variant_name { get; set; }
        public decimal? base_price { get; set; } // Giá gốc 
        public decimal? sale_price { get; set; } // Giá bán 
        public decimal? cost_price { get; set; } // Giá vốn 
        public bool is_default { get; set; } = false; // SKU mặc định
        public decimal? weight_grams { get; set; }
        public decimal? length_mm { get; set; }
        public decimal? width_mm { get; set; }
        public decimal? height_mm { get; set; }
        public string? attributes { get; set; } // JSON chứa các thuộc tính như Màu sắc, Kích thước
        public int? uom_id { get; set; }
        
        // Navigation
        public UomItem? uom { get; set; }
        public Product? product { get; set; }
        public ICollection<ProductMedia> media { get; set; } = new List<ProductMedia>();
        public ICollection<ProductLocationPricing> location_pricing { get; set; } = new List<ProductLocationPricing>();
    }



