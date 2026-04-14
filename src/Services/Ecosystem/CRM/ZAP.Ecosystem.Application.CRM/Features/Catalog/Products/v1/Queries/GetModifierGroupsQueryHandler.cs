using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.Queries;

public class GetModifierGroupsQueryHandler : IRequestHandler<GetModifierGroupsQuery, object>
{
    public Task<object> Handle(GetModifierGroupsQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Paged(new ZAP.Ecosystem.Shared.Data.PagedResult<ZAP.Ecosystem.Application.CRM.Features.ModifierGroup.v1.DTOs.ModifierGroupDto>()));
}
