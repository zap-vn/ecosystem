using System.Text.Json.Serialization;

namespace ZAP.Ecosystem.Application.CRM.Features.GeoCountry.v1.DTOs
{
    public class GeoCountryListRequestDto
    {
        [JsonPropertyName("page_index")]
        public int PageIndex { get; set; } = 1;

        [JsonPropertyName("page_size")]
        public int PageSize { get; set; } = 10;

        [JsonPropertyName("filters")]
        public GeoCountryListFilterDto Filters { get; set; } = new();

        [JsonPropertyName("sort")]
        public GeoCountryListSortDto Sort { get; set; } = new();
    }

    public class GeoCountryListFilterDto
    {
        [JsonPropertyName("is_active")]
        public bool? IsActive { get; set; }

        [JsonPropertyName("search")]
        public string? Search { get; set; }
    }

    public class GeoCountryListSortDto
    {
        [JsonPropertyName("field")]
        public string Field { get; set; } = "id";

        [JsonPropertyName("descending")]
        public bool Descending { get; set; } = false;
    }
}
