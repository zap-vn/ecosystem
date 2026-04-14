using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Menu.v1.Queries;

public class GetMenuListQueryHandler : IRequestHandler<GetMenuListQuery, object>
{
    public Task<object> Handle(GetMenuListQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Paged(Array.Empty<object>(), 0, request.Request.PageIndex, request.Request.PageSize));
}
