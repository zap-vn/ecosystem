using Microsoft.EntityFrameworkCore;
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
    [Table("product_location_pricing", Schema = "catalog")]
    [PrimaryKey(nameof(product_variant_id), nameof(location_id))]
    public class ProductLocationPricing
    {
        [Column("product_variant_id")]
        public Guid product_variant_id { get; set; }
        
        [Column("location_id")]
        public Guid location_id { get; set; }
        
        [Column("sale_price_override")]
        public decimal? sale_price_override { get; set; }
        public bool is_active { get; set; } = true;

        // Navigation
        public ProductVariant? variant { get; set; }
    }






