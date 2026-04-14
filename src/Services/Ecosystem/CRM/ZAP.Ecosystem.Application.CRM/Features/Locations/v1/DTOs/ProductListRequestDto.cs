using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.DTOs
{
    // Renamed from ProductListRequestDto to avoid conflict with Products.v1.DTOs
    public class LocProductListRequestDto
    {
        [JsonPropertyName("page_index")]
        public int Page { get; set; } = 1;

        [JsonPropertyName("page_size")]
        public int PageSize { get; set; } = 10;

        [JsonPropertyName("search")]
        public string Search { get; set; } = string.Empty;

        [JsonPropertyName("filters")]
        public LocProductListFilterDto Filters { get; set; } = new();

        [JsonPropertyName("sort")]
        public LocProductListSortDto Sort { get; set; } = new();
    }

    public class LocProductListFilterDto
    {
        [JsonPropertyName("cate_id")]
        public string? CategoryId { get; set; }

        [JsonPropertyName("status")]
        public int? StatusId { get; set; }

        [JsonPropertyName("location_id")]
        public string? LocationId { get; set; }

        [JsonPropertyName("locale_id")]
        public int? LocaleId { get; set; }

        [JsonPropertyName("product_type_id")]
        public int? ProductTypeId { get; set; }
    }

    public class LocProductListSortDto
    {
        [JsonPropertyName("field")]
        public string Field { get; set; } = "created_at";

        [JsonPropertyName("descending")]
        public bool Descending { get; set; } = true;
    }
}
