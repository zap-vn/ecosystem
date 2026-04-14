using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Sales.Application.Features.Reports.Queries.GetOverviewListLocation;

public class GetOverviewListLocationQueryHandler : IRequestHandler<GetOverviewListLocationQuery, object>
{
    public Task<object> Handle(GetOverviewListLocationQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
