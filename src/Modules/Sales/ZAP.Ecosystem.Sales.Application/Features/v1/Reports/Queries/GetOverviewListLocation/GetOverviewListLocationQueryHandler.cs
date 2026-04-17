using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Sales.Application.Features.Sales.v1.Reports.Queries.GetOverviewListLocation;

public class GetOverviewListLocationQueryHandler : IRequestHandler<GetOverviewListLocationQuery, object>
{
    public Task<object> Handle(GetOverviewListLocationQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}




