using ZAP.CRM.Catalog.Domain.Interfaces.Brands;
using ZAP.CRM.Catalog.Domain.Interfaces.Products;
using ZAP.CRM.Catalog.Domain.Interfaces.Menus;
using ZAP.CRM.Catalog.Domain.Interfaces.Categories;
using ZAP.CRM.Catalog.Domain.Interfaces.Locations;
using ZAP.CRM.Catalog.Domain.Interfaces.Modifiers;
using ZAP.CRM.Catalog.Domain.Interfaces.Geography;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using ZAP.Ecosystem.Shared.Entities;
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
namespace ZAP.CRM.Catalog.Application.Features.Geography.v1.DTOs;
    public class GeoCountryListRequestDto
    {
        [JsonProperty("page_index")]
        public int PageIndex { get; set; } = 1;

        [JsonProperty("page_size")]
        public int PageSize { get; set; } = 10;

        [JsonProperty("filters")]
        public GeoCountryListFilterDto Filters { get; set; } = new();

        [JsonProperty("sort")]
        public GeoCountryListSortDto Sort { get; set; } = new();
    }

    public class GeoCountryListFilterDto
    {
        [JsonProperty("is_active")]
        public bool? IsActive { get; set; }

        [JsonProperty("search")]
        public string? Search { get; set; }
    }

    public class GeoCountryListSortDto
    {
        [JsonProperty("field")]
        public string Field { get; set; } = "id";

        [JsonProperty("descending")]
        public bool Descending { get; set; } = false;
    }



