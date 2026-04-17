using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using ZAP.CRM.Catalog.Domain.Entities.Commons;

namespace ZAP.CRM.Catalog.Domain.Entities.Products;
    [Table("product_variant", Schema = "catalog")]
    public class ProductVariant
    {
        [Key]
        [Column("id")]
        public Guid id { get; set; } = Guid.NewGuid();

        [Column("serial_id")]
        public int? serial_id { get; set; }

        [Column("product_id")]
        public Guid? product_id { get; set; }

        [Column("sku_code")]
        public string? sku_code { get; set; }

        [Column("barcode")]
        public string? barcode { get; set; }

        [Column("variant_name")]
        public string? variant_name { get; set; }

        [Column("base_price")]
        public decimal? base_price { get; set; }

        [Column("sale_price")]
        public decimal? sale_price { get; set; }

        [Column("cost_price")]
        public decimal? cost_price { get; set; }

        [Column("is_default")]
        public bool is_default { get; set; } = false;

        [Column("weight_grams")]
        public decimal? weight_grams { get; set; }

        [Column("attributes")]
        public string? attributes { get; set; }

        [Column("uom_id")]
        public int? uom_id { get; set; }

        [Column("status_id")]
        public int? status_id { get; set; }
        
        // Navigation
        [ForeignKey("uom_id")]
        public UomItem? uom { get; set; }

        [ForeignKey("product_id")]
        public Product? product { get; set; }

        [NotMapped]
        public ICollection<ProductMedia> media { get; set; } = new List<ProductMedia>();

        public ICollection<ProductLocationPricing> location_pricing { get; set; } = new List<ProductLocationPricing>();
    }
