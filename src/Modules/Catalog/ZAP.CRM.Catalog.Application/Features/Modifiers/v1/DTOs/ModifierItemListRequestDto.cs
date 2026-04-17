using ZAP.CRM.Catalog.Domain.Interfaces.Brands;
using ZAP.CRM.Catalog.Domain.Interfaces.Products;
using ZAP.CRM.Catalog.Domain.Interfaces.Menus;
using ZAP.CRM.Catalog.Domain.Interfaces.Categories;
using ZAP.CRM.Catalog.Domain.Interfaces.Locations;
using ZAP.CRM.Catalog.Domain.Interfaces.Modifiers;
using ZAP.CRM.Catalog.Domain.Interfaces.Geography;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using ZAP.Ecosystem.Shared.Entities;
using System;
using Newtonsoft.Json;

using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Menus;
using ZAP.CRM.Catalog.Domain.Entities.Categories;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using ZAP.CRM.Catalog.Domain.Entities.Modifiers;
using ZAP.CRM.Catalog.Domain.Entities.Geography;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using ZAP.CRM.Catalog.Domain.Interfaces;
namespace ZAP.CRM.Catalog.Application.Features.Modifiers.v1.DTOs;
    public class ModifierItemListRequestDto
    {
        [JsonProperty("page_index")]
        public int PageIndex { get; set; } = 1;

        [JsonProperty("page_size")]
        public int PageSize { get; set; } = 10;

        [JsonProperty("filters")]
        public ModifierItemListFilterDto Filters { get; set; } = new();

        [JsonProperty("sort")]
        public ModifierItemListSortDto Sort { get; set; } = new();
    }

    public class ModifierItemListFilterDto
    {
        /// <summary>Filter by group_id. Null = all.</summary>
        [JsonProperty("group_id")]
        public Guid? GroupId { get; set; }

        /// <summary>Filter by status_id. Null = all.</summary>
        [JsonProperty("status_id")]
        public int? StatusId { get; set; }
    }

    /// <summary>Sort config. field: "sort_order" (default) | "created_at"</summary>
    public class ModifierItemListSortDto
    {
        [JsonProperty("field")]
        public string Field { get; set; } = "sort_order";

        [JsonProperty("descending")]
        public bool Descending { get; set; } = false;
    }



