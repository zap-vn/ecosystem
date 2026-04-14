using System;

namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.DTOs
{
    // Legacy class — see Management.v1.DTOs.PriceListDto
    internal class LegacyPriceListDto
    {
        public Guid id { get; set; }
        public string name { get; set; } = string.Empty;
    }
}
