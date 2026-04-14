using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Reports.v1.Queries;

public class GetReportListQuery : IRequest<object>
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? Search { get; set; }
}
