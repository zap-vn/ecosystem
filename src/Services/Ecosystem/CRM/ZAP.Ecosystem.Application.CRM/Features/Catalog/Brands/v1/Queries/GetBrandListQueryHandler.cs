using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Brands.v1.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Brands.v1.Queries;

public class GetBrandListQueryHandler : IRequestHandler<GetBrandListQuery, object>
{
    public async Task<object> Handle(GetBrandListQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var empty = new PagedResult<BrandDto>(new List<BrandDto>(), 0, request.Request.Page, request.Request.PageSize);
        return CrmResponse.Paged(empty);
    }
}
