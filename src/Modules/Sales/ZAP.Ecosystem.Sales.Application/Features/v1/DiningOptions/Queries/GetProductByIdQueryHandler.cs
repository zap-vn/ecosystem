using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Sales.Application.Features.Sales.v1.DiningOptions.Queries;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, object>
{
    public Task<object> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}




