using MediatR;
using ZAP.Ecosystem.Shared.Responses;

namespace ZAP.CRM.Catalog.Application.Features.Commons.Units;

public class GetUnitsListQuery : IRequest<object>
{
    public int page_index { get; set; } = 1;
    public int page_size { get; set; } = 10;
    public string? search { get; set; }
    public int? status_id { get; set; }
    public int? precision { get; set; }
}
