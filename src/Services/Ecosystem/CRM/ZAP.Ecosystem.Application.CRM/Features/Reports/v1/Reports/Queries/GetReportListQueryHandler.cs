using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Reports.v1.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Reports.v1.Queries;

public class GetReportListQueryHandler : IRequestHandler<GetReportListQuery, object>
{
    public Task<object> Handle(GetReportListQuery request, CancellationToken cancellationToken)
    {
        var dtos = new List<ReportDto>();
        return Task.FromResult(CrmResponse.Paged(new ZAP.Ecosystem.Shared.Data.PagedResult<ReportDto>(dtos, 0, request.PageIndex, request.PageSize)));
    }
}
