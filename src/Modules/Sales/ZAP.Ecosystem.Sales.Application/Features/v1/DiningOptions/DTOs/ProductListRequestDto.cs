using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZAP.Ecosystem.Sales.Application.Features.Sales.v1.DiningOptions.DTOs;
    public class ProductListRequestDto
    {
        [JsonProperty("page_index")]
        public int Page { get; set; } = 1;

        [JsonProperty("page_size")]
        public int PageSize { get; set; } = 10;

        [JsonProperty("search")]
        public string Search { get; set; } = string.Empty;

        [JsonProperty("filters")]
        public ProductListFilterDto Filters { get; set; } = new();

        [JsonProperty("sort")]
        public ProductListSortDto Sort { get; set; } = new();
    }

    public class ProductListFilterDto
    {
        [JsonProperty("cate_id")]
        public string? CategoryId { get; set; }

        [JsonProperty("status")]
        public int? StatusId { get; set; }

        [JsonProperty("location_id")]
        public string? LocationId { get; set; }

        [JsonProperty("locale_id")]
        public int? LocaleId { get; set; }

        [JsonProperty("product_type_id")]
        public int? ProductTypeId { get; set; }
    }

    /// <summary>
    /// Sort config. field: "name" | "price" | "stock" | "created_at" (default)
    /// </summary>
    public class ProductListSortDto
    {
        [JsonProperty("field")]
        public string Field { get; set; } = "created_at";

        [JsonProperty("descending")]
        public bool Descending { get; set; } = true;
    }




