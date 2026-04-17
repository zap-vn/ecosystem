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
using System;
using System.Text.Json;

using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Menus;
using ZAP.CRM.Catalog.Domain.Entities.Categories;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using ZAP.CRM.Catalog.Domain.Entities.Modifiers;
using ZAP.CRM.Catalog.Domain.Entities.Geography;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using ZAP.CRM.Catalog.Domain.Interfaces;
namespace ZAP.CRM.Catalog.Application.Features.Locations.v1.Commands;
    public class UpdateLocationCommand : IRequest<object>
    {
        public Guid Id { get; set; }
        public string? legacy_id { get; set; }
        public string? name { get; set; }
        public string? serial_number { get; set; }
        public string? location_code { get; set; }
        public int? status_id { get; set; }
        public bool? is_active { get; set; }
        public string? slug { get; set; }
        public string? business_name { get; set; }
        public string? description { get; set; }
        public int? location_type_id { get; set; }
        public string? address_line_1 { get; set; }
        public string? address_line_2 { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public int? country_id { get; set; }
        public int? province_id { get; set; }
        public int? district_id { get; set; }
        public int? ward_id { get; set; }
        public string? zipcode { get; set; }
        public string? phone_number { get; set; }
        public string? email { get; set; }
        public string? website { get; set; }
        public string? twitter { get; set; }
        public string? instagram { get; set; }
        public string? facebook { get; set; }
        public string? logo_url { get; set; }
        public string? cover_image_url { get; set; }
        public string? brand_color { get; set; }
        public string? timezone { get; set; }
        public decimal? latitude { get; set; }
        public decimal? longitude { get; set; }
        public JsonElement? operating_hours { get; set; }
        public string? transfer_account { get; set; }
        public string? transfer_tag { get; set; }
        public Guid? parent_location_id { get; set; }
    }



