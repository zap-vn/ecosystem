using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Brands.v1.Queries;

public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, object>
{
    public async Task<object> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return CrmResponse.NotFound("Brand");
    }
}
