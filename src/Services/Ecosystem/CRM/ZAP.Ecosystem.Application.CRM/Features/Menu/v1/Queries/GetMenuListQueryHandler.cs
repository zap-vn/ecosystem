using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ZAP.Ecosystem.Shared.Data;

namespace ZAP.Ecosystem.Application.CRM.Features.Menu.v1.Queries;

public class GetMenuListQueryHandler : IRequestHandler<GetMenuListQuery, object>
{
    public Task<object> Handle(GetMenuListQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<object>(new {
            success = true,
            code = 200,
            message = "OK",
            data = new { total_page = 0, total_record = 0, page_index = 1, page_size = 10, items = System.Array.Empty<object>() }
        });
    }
}
