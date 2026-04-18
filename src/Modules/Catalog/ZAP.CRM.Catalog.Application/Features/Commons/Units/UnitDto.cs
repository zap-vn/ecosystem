using System;

namespace ZAP.CRM.Catalog.Application.Features.Commons.Units;

public class UnitDto
{
    public int id { get; set; }
    public string? serial_id { get; set; } // Mapping from legacy_id if exists
    public string code { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public string symbol { get; set; } = string.Empty; // From code or acronymn
    public int precision { get; set; }
    public bool is_active { get; set; }
    public ZAP.CRM.Catalog.Application.Features.Commons.DTOs.StatusDto? status { get; set; }
}
