using System;
using System.Collections.Generic;

namespace ZAP.Ecosystem.Application.CRM.Features.Collections.v1.DTOs
{
    public class CollectionDto
    {
        public Guid id { get; set; }
        public int? serial_id { get; set; }
        public string name { get; set; } = string.Empty;
        public string? image_url { get; set; }
        public int product_count { get; set; }
        public int status_id { get; set; }
        public string? status_code { get; set; }
        public string? status_name { get; set; }
    }

    public class CollectionItemDto
    {
        public Guid product_id { get; set; }
        public string? product_name { get; set; }
        public int sort_order { get; set; }
    }

    public class CollectionListRequestDto
    {
        public int page_index { get; set; } = 1;
        public int page_size { get; set; } = 10;
        public string? search { get; set; }
    }
}


