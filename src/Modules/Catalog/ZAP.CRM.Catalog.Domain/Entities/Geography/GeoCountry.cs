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

namespace ZAP.CRM.Catalog.Domain.Entities.Geography;
    [Table("geo_country", Schema = "system")]
    public class GeoCountry
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("serial_id")]
        public int? serial_id { get; set; }

        [Column("serial_number")]
        public string? serial_number { get; set; }

        [Column("iso_alpha2")]
        public string? iso_alpha2 { get; set; }

        [Column("iso_alpha3")]
        public string? iso_alpha3 { get; set; }

        [Column("numeric_code")]
        public int? numeric_code { get; set; }

        [Column("is_active")]
        public bool is_active { get; set; }

        [Column("latitude")]
        public decimal? latitude { get; set; }

        [Column("longitude")]
        public decimal? longitude { get; set; }

        [Column("geometry_data", TypeName = "text")]
        public string? geometry_data { get; set; }

        [Column("flag_emoji")]
        public string? flag_emoji { get; set; }

        [Column("flag_url")]
        public string? flag_url { get; set; }

        [Column("created_at")]
        public DateTime created_at { get; set; }

        [Column("updated_at")]
        public DateTime? updated_at { get; set; }

        public virtual System.Collections.Generic.ICollection<GeoCountryTranslation> Translations { get; set; } = new System.Collections.Generic.List<GeoCountryTranslation>();
    }




