using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZAP.CRM.Catalog.Domain.Entities.Products;
    [Table("product_location_pricing", Schema = "catalog")]
    public class ProductLocationPricing
    {
        [Key]
        [Column("id")]
        public Guid id { get; set; }

        [Column("product_variant_id")]
        public Guid product_variant_id { get; set; }
        
        [Column("warehouse_id")]
        public Guid warehouse_id { get; set; }
        
        [Column("price_override")]
        public decimal? price_override { get; set; }

        [Column("sale_price_override")]
        public decimal? sale_price_override { get; set; }

        [Column("is_active")]
        public bool is_active { get; set; } = true;

        [Column("last_updated_at")]
        public DateTime? last_updated_at { get; set; }

        // Navigation
        [ForeignKey("product_variant_id")]
        public ProductVariant? variant { get; set; }
    }
