using ZAP.CRM.Catalog.Domain.Interfaces.Brands;
using ZAP.CRM.Catalog.Domain.Interfaces.Products;
using ZAP.CRM.Catalog.Domain.Interfaces.Menus;
using ZAP.CRM.Catalog.Domain.Interfaces.Categories;
using ZAP.CRM.Catalog.Domain.Interfaces.Locations;
using ZAP.CRM.Catalog.Domain.Interfaces.Modifiers;
using ZAP.CRM.Catalog.Domain.Interfaces.Geography;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using ZAP.Ecosystem.Shared.Entities;
using ZAP.Ecosystem.Shared.Data;
using ZAP.Ecosystem.Shared.Interfaces;
using ZAP.CRM.Catalog.Domain;
using MediatR;
using System.Collections.Generic;
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
namespace ZAP.CRM.Catalog.Application.Features.Locations.v1.DTOs;
    public class LocationListRequestDto
    {
        public int page_index { get; set; } = 1;
        public int page_size { get; set; } = 10;
        public string? search { get; set; }
        public LocationFiltersDto? filters { get; set; }
        public LocationSortDto? sort { get; set; }
        public int locale_id { get; set; } = 2; // Default to 2 (VI) or 1 (EN)
    }

    public class LocationFiltersDto
    {
        /// <summary>Tr?ng th�i (status_id): ACTIVE / INACTIVE</summary>
        public int? status_id { get; set; }

        /// <summary>Th�nh ph? / t?nh (province_id)</summary>
        public int? province_id { get; set; }

        /// <summary>Lo?i h�nh v? tr�: truy?n m?ng int, VD: [1, 2]</summary>
        [JsonProperty("location_type_id")]
        public List<int>? location_type_id { get; set; }
    }

    public class LocationSortDto
    {
        /// <summary>Field to sort by: "name" | "status"</summary>
        public string? field { get; set; }

        /// <summary>true = descending (Z-A), false = ascending (A-Z)</summary>
        public bool descending { get; set; } = false;
    }



