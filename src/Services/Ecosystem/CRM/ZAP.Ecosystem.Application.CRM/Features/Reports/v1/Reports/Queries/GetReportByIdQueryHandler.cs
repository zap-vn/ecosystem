using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Reports.v1.Queries;

public class GetReportByIdQueryHandler : IRequestHandler<GetReportByIdQuery, object>
{
    public Task<object> Handle(GetReportByIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(CrmResponse.NotFound("Report"));
    }
}
