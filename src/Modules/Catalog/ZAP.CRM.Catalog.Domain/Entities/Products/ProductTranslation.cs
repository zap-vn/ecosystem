using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZAP.Ecosystem.Shared.Entities;

namespace ZAP.CRM.Catalog.Domain.Entities.Products;
    [Table("product_translation", Schema = "catalog")]
    public class ProductTranslation : BaseTranslationEntity
    {
        [Column("product_id")]
        public Guid product_id { get; set; }

        [Column("locale_id")]
        public int locale_id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("short_description")]
        public string? ShortDescription { get; set; }

        [Column("long_description_html")]
        public string? LongDescriptionHtml { get; set; }

        // Navigation
        [ForeignKey("product_id")]
        public Product? product { get; set; }
    }
