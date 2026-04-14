using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Categories.v1.Queries;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, object>
{
    public Task<object> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
