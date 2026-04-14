using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Queries;

public class GetLocProductByIdQueryHandler : IRequestHandler<GetLocProductByIdQuery, object>
{
    public Task<object> Handle(GetLocProductByIdQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.NotFound("Product"));
}
