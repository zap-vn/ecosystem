using System;
using System.Text.Json.Serialization;

namespace ZAP.Ecosystem.Application.CRM.Features.ModifierItem.v1.DTOs
{
    public class ModifierItemListRequestDto
    {
        [JsonPropertyName("page_index")]
        public int PageIndex { get; set; } = 1;

        [JsonPropertyName("page_size")]
        public int PageSize { get; set; } = 10;

        [JsonPropertyName("filters")]
        public ModifierItemListFilterDto Filters { get; set; } = new();

        [JsonPropertyName("sort")]
        public ModifierItemListSortDto Sort { get; set; } = new();
    }

    public class ModifierItemListFilterDto
    {
        /// <summary>Filter by group_id. Null = all.</summary>
        [JsonPropertyName("group_id")]
        public Guid? GroupId { get; set; }

        /// <summary>Filter by status_id. Null = all.</summary>
        [JsonPropertyName("status_id")]
        public int? StatusId { get; set; }
    }

    /// <summary>Sort config. field: "sort_order" (default) | "created_at"</summary>
    public class ModifierItemListSortDto
    {
        [JsonPropertyName("field")]
        public string Field { get; set; } = "sort_order";

        [JsonPropertyName("descending")]
        public bool Descending { get; set; } = false;
    }
}
