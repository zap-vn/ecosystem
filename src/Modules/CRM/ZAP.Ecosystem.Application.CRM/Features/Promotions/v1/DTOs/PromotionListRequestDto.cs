using Newtonsoft.Json;

namespace ZAP.Ecosystem.CRM.Application.Features.Promotions.v1.DTOs;
    public class PromotionListRequestDto
    {
        [JsonProperty("page_index")]
        public int PageIndex { get; set; } = 1;

        [JsonProperty("page_size")]
        public int PageSize { get; set; } = 10;

        [JsonProperty("search")]
        public string? Search { get; set; }

        [JsonProperty("filters")]
        public PromotionListFilterDto Filters { get; set; } = new();

        [JsonProperty("sort")]
        public PromotionListSortDto Sort { get; set; } = new();
    }

    public class PromotionListFilterDto
    {
        [JsonProperty("status_id")]
        public int? StatusId { get; set; }

        /// <summary>3101 PERCENTAGE | 3102 FIXED_AMOUNT | 3103 SPECIFIC_PRICE | 3104 FREE_ITEM</summary>
        [JsonProperty("discount_type_id")]
        public int? DiscountTypeId { get; set; }

        /// <summary>Placeholder: no member_level_id column in promotion table yet.</summary>
        [JsonProperty("member_level_id")]
        public int? MemberLevelId { get; set; }

        /// <summary>true = applies to all locations; false = location-specific.</summary>
        [JsonProperty("is_all_locations")]
        public bool? IsAllLocations { get; set; }

        /// <summary>true = auto-apply; false = requires manual code entry.</summary>
        [JsonProperty("is_automatic")]
        public bool? IsAutomatic { get; set; }
    }

    /// <summary>
    /// Sort config. field: "name" (default) | "discount_value" | "status_id"
    /// </summary>
    public class PromotionListSortDto
    {
        [JsonProperty("field")]
        public string Field { get; set; } = "name";

        [JsonProperty("descending")]
        public bool Descending { get; set; } = false;
    }




