using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Locations.v1.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Queries;

public class GetLocProductListQueryHandler : IRequestHandler<GetLocProductListQuery, object>
{
    public Task<object> Handle(GetLocProductListQuery request, CancellationToken cancellationToken)
    {
        var dtos = new List<LocProductDto>();
        return Task.FromResult(CrmResponse.Paged(new ZAP.Ecosystem.Shared.Data.PagedResult<LocProductDto>(dtos, 0, request.Request.Page, request.Request.PageSize)));
    }
}
