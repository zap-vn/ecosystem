using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.Queries;

public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, object>
{
    public Task<object> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
