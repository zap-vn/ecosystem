using System.Text.Json.Serialization;

namespace ZAP.Ecosystem.Application.CRM.Features.Menu.v1.DTOs
{
    // Renamed from ProductListRequestDto to avoid conflict with Products.v1.DTOs
    public class MenuProductListRequestDto
    {
        [JsonPropertyName("page_index")]
        public int Page { get; set; } = 1;

        [JsonPropertyName("page_size")]
        public int PageSize { get; set; } = 10;

        [JsonPropertyName("search")]
        public string Search { get; set; } = string.Empty;

        [JsonPropertyName("filters")]
        public MenuProductListFilterDto Filters { get; set; } = new();

        [JsonPropertyName("sort")]
        public MenuProductListSortDto Sort { get; set; } = new();
    }

    public class MenuProductListFilterDto
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

    public class MenuProductListSortDto
    {
        [JsonPropertyName("field")]
        public string Field { get; set; } = "created_at";

        [JsonPropertyName("descending")]
        public bool Descending { get; set; } = true;
    }
}
