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

namespace ZAP.CRM.Catalog.Domain.Entities.Categories;
    [Table("category", Schema = "catalog")]
    public class Category
    {
        [Column("id")]
        public Guid id { get; set; }

        [Column("tenant_id")]
        public Guid? tenant_id { get; set; }

        [Column("parent_id")]
        public Guid? parent_id { get; set; }

        [Column("serial_id")]
        public int? serial_id { get; set; }

        [Column("serial_number")]
        public string? serial_number { get; set; }

        [Column("category_code")]
        public string? category_code { get; set; }

        [Column("legacy_id")]
        public string? legacy_id { get; set; }

        [Column("materialized_path")]
        public string? materialized_path { get; set; }

        [NotMapped]
        public string name { get; set; } = string.Empty;

        [Column("slug")]
        public string? slug { get; set; }

        [Column("icon_url")]
        public string? icon_url { get; set; }

        [Column("banner_url")]
        public string? banner_url { get; set; }

        [Column("sort_order")]
        public int? sort_order { get; set; }

        [NotMapped]
        public string? meta_title { get; set; }

        [NotMapped]
        public string? meta_description { get; set; }

        [NotMapped]
        public string? meta_keywords { get; set; }

        [Column("canonical_url")]
        public string? canonical_url { get; set; }

        [Column("status_id")]
        public int? status_id { get; set; }

        [Column("is_active")]
        public bool is_active { get; set; } = true;

        [NotMapped]
        public string? display_initial { get; set; }

        [Column("applicable_channels")]
        public int[]? applicable_channels { get; set; }

        [NotMapped]
        public string? seo_title { get; set; }

        [NotMapped]
        public string? seo_description { get; set; }

        [NotMapped]
        public string? status_code { get; set; }

        [NotMapped]
        public string? status_name { get; set; }

        // Navigation properties (Renamed to avoid EF conventions)
        [ForeignKey("parent_id")]
        public Category? parent_category { get; set; }

        [ForeignKey("status_id")]
        public StatusItem? status { get; set; }
        
        public ICollection<Category> sub_categories { get; set; } = new List<Category>();
        
        [NotMapped]
        public ICollection<ProductCategoryMap> product_mappings { get; set; } = new List<ProductCategoryMap>();

        public ICollection<CategoryTranslation> translations { get; set; } = new List<CategoryTranslation>();
    }

    [Table("category_translation", Schema = "catalog")]
    public class CategoryTranslation
    {
        [Key]
        [Column("id")]
        public Guid id { get; set; }
        
        [Column("category_id")]
        public Guid category_id { get; set; }
        
        [Column("locale_id")]
        public int locale_id { get; set; }
        
        [Column("name")]
        public string? Name { get; set; }
        
        [Column("seo_title")]
        public string? SeoTitle { get; set; }
        
        [Column("seo_description")]
        public string? SeoDescription { get; set; }
        
        [Column("meta_title")]
        public string? MetaTitle { get; set; }
        
        [Column("meta_description")]
        public string? MetaDescription { get; set; }

        [ForeignKey("category_id")]
        public Category? category { get; set; }
    }
