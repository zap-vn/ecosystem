using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.DiningOptions.v1.Queries;

public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, object>
{
    public Task<object> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
