using ZAP.CRM.Catalog.Application.Features.Products.v1.DTOs;
using ZAP.Ecosystem.Shared.Responses;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.CRM.Catalog.Application.Features.Products.v1.Queries;

public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, object>
{
    public Task<object> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var dtos = new List<ProductDto>();
        return Task.FromResult(CrmResponse.Paged(new ZAP.Ecosystem.Shared.Data.PagedResult<ProductDto>(dtos, 0, request.Request.PageIndex, request.Request.PageSize)));
    }
}



