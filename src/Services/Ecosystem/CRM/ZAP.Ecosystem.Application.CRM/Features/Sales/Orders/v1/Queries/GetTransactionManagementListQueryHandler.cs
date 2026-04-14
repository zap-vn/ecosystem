using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Orders.v1.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Orders.v1.Queries;

public class GetTransactionManagementListQueryHandler : IRequestHandler<GetTransactionManagementListQuery, object>
{
    public Task<object> Handle(GetTransactionManagementListQuery request, CancellationToken cancellationToken)
    {
        var dtos = new List<TransactionListDto>();
        return Task.FromResult(CrmResponse.Paged(new ZAP.Ecosystem.Shared.Data.PagedResult<TransactionListDto>(dtos, 0, request.Page, request.PageSize)));
    }
}
