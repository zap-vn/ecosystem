using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Queries;

public class GetPaymentTermsByIdQueryHandler : IRequestHandler<GetPaymentTermsByIdQuery, object>
{
    public Task<object> Handle(GetPaymentTermsByIdQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.NotFound("PaymentTerms"));
}
