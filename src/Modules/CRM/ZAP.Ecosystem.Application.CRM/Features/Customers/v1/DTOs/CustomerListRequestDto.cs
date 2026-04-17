using Newtonsoft.Json;

namespace ZAP.Ecosystem.CRM.Application.Features.Customers.v1.DTOs;
    public class CustomerListRequestDto
    {
        [JsonProperty("page_index")]
        public int PageIndex { get; set; } = 1;

        [JsonProperty("page_size")]
        public int PageSize { get; set; } = 20;

        /// <summary>Searches across full_name, phone_number, legacy_id, and id (Guid).</summary>
        [JsonProperty("search")]
        public string? Search { get; set; }

        [JsonProperty("filters")]
        public CustomerListFilterDto Filters { get; set; } = new();

        [JsonProperty("sort")]
        public CustomerListSortDto Sort { get; set; } = new();
    }

    public class CustomerListFilterDto
    {
        [JsonProperty("status_id")]
        public int? StatusId { get; set; }

        [JsonProperty("tier_id")]
        public Guid? TierId { get; set; }

        /// <summary>Filter: total_spent_amount >= value.</summary>
        [JsonProperty("min_total_spent")]
        public decimal? MinTotalSpent { get; set; }

        /// <summary>Filter: total_spent_amount <= value.</summary>
        [JsonProperty("max_total_spent")]
        public decimal? MaxTotalSpent { get; set; }

        /// <summary>Filter: current_points_balance >= value.</summary>
        [JsonProperty("min_points")]
        public decimal? MinPoints { get; set; }

        /// <summary>Filter: current_points_balance <= value.</summary>
        [JsonProperty("max_points")]
        public decimal? MaxPoints { get; set; }

        /// <summary>Placeholder for wallet balance filter � no current_balance column yet.</summary>
        [JsonProperty("min_balance")]
        public decimal? MinBalance { get; set; }

        /// <summary>Placeholder for wallet balance filter � no current_balance column yet.</summary>
        [JsonProperty("max_balance")]
        public decimal? MaxBalance { get; set; }
    }

    /// <summary>
    /// Sort config.
    /// field: "full_name" (default) | "total_spent_amount" | "current_points_balance" | "tier_id" | "status_id"
    /// </summary>
    public class CustomerListSortDto
    {
        [JsonProperty("field")]
        public string Field { get; set; } = "full_name";

        [JsonProperty("descending")]
        public bool Descending { get; set; } = false;
    }




