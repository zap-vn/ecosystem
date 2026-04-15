using System;

namespace ZAP.Ecosystem.Application.CRM.Features.GeoCountry.v1.DTOs
{
    public class GeoCountryDto
    {
        public int id { get; set; }
        public int? serial_id { get; set; }
        public string? serial_number { get; set; }
        public string? iso_alpha2 { get; set; }
        public string? iso_alpha3 { get; set; }
        public int? numeric_code { get; set; }
        public bool is_active { get; set; }
        public decimal? latitude { get; set; }
        public decimal? longitude { get; set; }
        public string? geometry_data { get; set; }
        public string? flag_emoji { get; set; }
        public string? flag_url { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
