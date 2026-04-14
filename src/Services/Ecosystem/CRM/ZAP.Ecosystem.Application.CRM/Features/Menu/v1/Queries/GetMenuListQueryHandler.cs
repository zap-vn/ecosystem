using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ZAP.Ecosystem.Shared.Data;

namespace ZAP.Ecosystem.Application.CRM.Features.Menu.v1.Queries;

public class GetMenuListQueryHandler : IRequestHandler<GetMenuListQuery, object>
{
    public Task<object> Handle(GetMenuListQuery request, CancellationToken cancellationToken)
    {
        var paged = new PagedResult<object>(
            System.Array.Empty<object>(), 0,
            request.Request.PageIndex,
            request.Request.PageSize);
        return Task.FromResult(CrmResponse.Paged(paged));
    }
}
