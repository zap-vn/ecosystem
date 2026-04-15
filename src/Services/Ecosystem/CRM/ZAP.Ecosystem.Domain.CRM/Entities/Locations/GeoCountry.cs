using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZAP.Ecosystem.Domain.CRM
{
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
        public string? numeric_code { get; set; }

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
    }
}
