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
namespace ZAP.CRM.Catalog.Application.Features.Products.v1.DTOs;

public class ProductListRequestDto
{
    [JsonProperty("page_index")]
    public int PageIndex { get; set; } = 1;

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

public class ProductListSortDto
{
    [JsonProperty("field")]
    public string Field { get; set; } = "created_at";

    [JsonProperty("descending")]
    public bool Descending { get; set; } = true;
}



