using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Payments.v1.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Queries;

public class GetPaymentTermsListQueryHandler : IRequestHandler<GetPaymentTermsListQuery, object>
{
    public Task<object> Handle(GetPaymentTermsListQuery request, CancellationToken cancellationToken)
    {
        var dtos = new List<PaymentTermsDto>();
        return Task.FromResult(CrmResponse.Paged(new ZAP.Ecosystem.Shared.Data.PagedResult<PaymentTermsDto>(dtos, 0, request.PageIndex, request.PageSize)));
    }
}
