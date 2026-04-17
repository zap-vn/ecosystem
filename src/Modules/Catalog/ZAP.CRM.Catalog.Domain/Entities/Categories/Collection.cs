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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZAP.CRM.Catalog.Domain.Entities.Categories;
    [Table("collection", Schema = "catalog")]
    public class Collection
    {
        [Key]
        [Column("id")]
        public Guid id { get; set; }
        
        [Column("serial_id")]
        public int? serial_id { get; set; }

        [Column("tenant_id")]
        public Guid? tenant_id { get; set; }

        [Column("name")]
        public string name { get; set; } = string.Empty;

        [Column("slug")]
        public string slug { get; set; } = string.Empty;

        [Column("description")]
        public string? description_html { get; set; }

        [Column("image_url")]
        public string? banner_url { get; set; }

        [Column("status_id")]
        public int status_id { get; set; } = 1;

        [Column("sort_order")]
        public int sort_order { get; set; } = 0;

        [Column("created_at")]
        public DateTime created_at { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? updated_at { get; set; }

        public ICollection<CollectionItem> items { get; set; } = new List<CollectionItem>();
        
        [ForeignKey("status_id")]
        public StatusItem? status { get; set; }

        [NotMapped]
        public string? status_code { get; set; }

        [NotMapped]
        public string? status_name { get; set; }
    }



