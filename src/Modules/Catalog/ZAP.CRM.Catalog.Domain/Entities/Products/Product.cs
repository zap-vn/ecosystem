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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZAP.CRM.Catalog.Domain.Entities.Products;
    [Table("product", Schema = "catalog")]
    public class Product
    {
        [Key]
        [Column("id")]
        public Guid id { get; set; } = Guid.NewGuid();
        [Column("serial_id")]
        public int? serial_id { get; set; }
        
        [Column("tenant_id")]
        public Guid? tenant_id { get; set; }
        
        [Column("brand_id")]
        public Guid? brand_id { get; set; }

        [Column("category_id")]
        public Guid? category_id { get; set; }
        
        public string? legacy_id { get; set; } // Dùng để đối chiếu với hệ thống cũ
        
        [Column("product_type_id")]
        public int product_type_id { get; set; } = 1; // 1: PHYSICAL, 2: SERVICE, 3: DIGITAL, 4: BUNDLE
        
        [NotMapped]
        public string name { get; set; } = string.Empty;
        
        [NotMapped]
        public string? short_description { get; set; }
        
        [NotMapped]
        public string? long_description_html { get; set; }
        
        public string? search_vector { get; set; }
        public int priority_score { get; set; } = 0;
        
        [Column("status_id")]
        public int? status_id { get; set; } // 2201 - Active, Draft, Archived
        
        public bool is_featured { get; set; } = false;
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime? updated_at { get; set; }

        // Navigation
        [ForeignKey("status_id")]
        public StatusItem? status { get; set; }
        
        [NotMapped]
        public ProductTypeItem? product_type { get; set; }

        [ForeignKey("category_id")]
        public Category? category { get; set; }

        [NotMapped]
        public string? status_code { get; set; }

        [NotMapped]
        public string? status_name { get; set; }
        public ICollection<ProductVariant> variants { get; set; } = new List<ProductVariant>();
        
        public ICollection<ProductCategoryMap> category_mappings { get; set; } = new List<ProductCategoryMap>();
        public ICollection<ProductTranslation> translations { get; set; } = new List<ProductTranslation>();
    }
