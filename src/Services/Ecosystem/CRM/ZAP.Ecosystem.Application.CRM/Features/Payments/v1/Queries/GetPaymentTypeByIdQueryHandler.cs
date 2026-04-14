using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Queries;

public class GetPaymentTypeByIdQueryHandler : IRequestHandler<GetPaymentTypeByIdQuery, object>
{
    public Task<object> Handle(GetPaymentTypeByIdQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.NotFound("PaymentType"));
}
