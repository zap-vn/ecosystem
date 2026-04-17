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
    public class GeoCountryDto
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int? serial_id { get; set; }
        public string? serial_number { get; set; }
        public string? iso_alpha2 { get; set; }
        public string? iso_alpha3 { get; set; }
        public int? numeric_code { get; set; }
        public bool is_active { get; set; }
        public decimal? latitude { get; set; }
        public decimal? longitude { get; set; }
        public string? geometry_data { get; set; }
        public string? flag_emoji { get; set; }
        public string? flag_url { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }



