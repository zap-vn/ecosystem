using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Management.v1.Queries;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, object>
{
    public Task<object> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
