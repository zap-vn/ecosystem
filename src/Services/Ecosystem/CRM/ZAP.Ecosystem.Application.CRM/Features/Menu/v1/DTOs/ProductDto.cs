using System;
using System.Collections.Generic;

namespace ZAP.Ecosystem.Application.CRM.Features.Menu.v1.DTOs
{
    // Renamed from ProductVariantDto/ProductDto to avoid conflict with Products.v1.DTOs
    public class MenuProductVariantDto
    {
        public Guid id { get; set; }
        public int? serial_id { get; set; }
        public string? sku_code { get; set; }
        public string? barcode { get; set; }
        public string? variant_name { get; set; }
        public decimal? base_price { get; set; }
        public decimal? sale_price { get; set; }
        public decimal? cost_price { get; set; }
        public bool is_active { get; set; }
        public string? media_url { get; set; }
        public decimal? qty_on_hand { get; set; }
    }

    public class MenuProductDto
    {
        public Guid id { get; set; }
        public string name { get; set; } = string.Empty;
        public int? status_id { get; set; }
        public List<MenuProductVariantDto> variants { get; set; } = new();
        public string? media_url { get; set; }
        public string? sku_code { get; set; }
        public decimal? sale_price { get; set; }
    }
}
