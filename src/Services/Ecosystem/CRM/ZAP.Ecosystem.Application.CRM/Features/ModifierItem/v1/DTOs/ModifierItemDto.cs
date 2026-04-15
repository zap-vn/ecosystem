using System;

namespace ZAP.Ecosystem.Application.CRM.Features.ModifierItem.v1.DTOs
{
    public class ModifierItemDto
    {
        public Guid id { get; set; }
        public int? serial_id { get; set; }
        public string? serial_number { get; set; }
        public Guid? group_id { get; set; }
        public Guid? product_variant_id { get; set; }
        public string? image_url { get; set; }
        public decimal? price_override { get; set; }
        public int sort_order { get; set; }
        public int? status_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
