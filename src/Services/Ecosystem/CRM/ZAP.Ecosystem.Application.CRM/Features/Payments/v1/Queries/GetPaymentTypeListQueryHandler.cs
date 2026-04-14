using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Payments.v1.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Queries;

public class GetPaymentTypeListQueryHandler : IRequestHandler<GetPaymentTypeListQuery, object>
{
    public Task<object> Handle(GetPaymentTypeListQuery request, CancellationToken cancellationToken)
    {
        var dtos = new List<PaymentTypeDto>();
        return Task.FromResult(CrmResponse.Paged(new ZAP.Ecosystem.Shared.Data.PagedResult<PaymentTypeDto>(dtos, 0, request.PageIndex, request.PageSize)));
    }
}
