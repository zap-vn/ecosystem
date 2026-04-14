#nullable enable
using System;
using System.Collections.Generic;

namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.DTOs
{
    // Legacy class — use Categories.v1.DTOs.CategoryDto instead
    internal class LegacyCategoryDto
    {
        public Guid id { get; set; }
        public string name { get; set; } = string.Empty;
    }
}
