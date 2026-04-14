using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.Queries;

public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, object>
{
    public Task<object> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Paged(new ZAP.Ecosystem.Shared.Data.PagedResult<ZAP.Ecosystem.Application.CRM.Features.Categories.v1.DTOs.CategoryDto>()));
}
