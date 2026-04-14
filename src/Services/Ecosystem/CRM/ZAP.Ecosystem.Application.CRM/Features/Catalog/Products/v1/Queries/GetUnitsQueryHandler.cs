using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.Queries;

public class GetUnitsQueryHandler : IRequestHandler<GetUnitsQuery, object>
{
    public Task<object> Handle(GetUnitsQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Paged(new ZAP.Ecosystem.Shared.Data.PagedResult<ZAP.Ecosystem.Application.CRM.Features.Units.v1.DTOs.UnitDto>()));
}
