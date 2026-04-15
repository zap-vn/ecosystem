using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZAP.Ecosystem.Domain.CRM
{
    [Table("geo_country_translation", Schema = "system")]
    public class GeoCountryTranslation
    {
        [Key]
        [Column("id")]
        public Guid id { get; set; }

        [Column("serial_id")]
        public int? serial_id { get; set; }

        [Column("serial_number")]
        public string? serial_number { get; set; }

        [Column("country_id")]
        public int country_id { get; set; }

        [Column("locale_id")]
        public int locale_id { get; set; }

        [Column("name")]
        public string name { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime created_at { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? updated_at { get; set; }

        [ForeignKey("country_id")]
        public virtual GeoCountry? Country { get; set; }
    }
}
