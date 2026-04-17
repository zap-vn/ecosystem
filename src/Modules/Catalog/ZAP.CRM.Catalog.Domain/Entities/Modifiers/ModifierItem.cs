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
    [Table("modifier_item", Schema = "catalog")]
    public class ModifierItem
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();

        [Column("serial_id")]
        public int? serial_id { get; set; }

        [Column("serial_number")]
        public string? serial_number { get; set; }

        [Column("group_id")]
        public Guid? group_id { get; set; }

        [Column("product_variant_id")]
        public Guid? product_variant_id { get; set; }

        [Column("image_url")]
        public string? image_url { get; set; }

        [Column("price_override")]
        public decimal? price_override { get; set; }

        [Column("sort_order")]
        public int sort_order { get; set; } = 0;

        [Column("status_id")]
        public int? status_id { get; set; }

        [Column("created_at")]
        public DateTime created_at { get; set; }

        [Column("updated_at")]
        public DateTime? updated_at { get; set; }

        [NotMapped]
        public string? status_code { get; set; }

        [NotMapped]
        public string? status_name { get; set; }
    }



