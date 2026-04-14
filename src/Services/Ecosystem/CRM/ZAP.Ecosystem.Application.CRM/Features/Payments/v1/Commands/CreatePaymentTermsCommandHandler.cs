using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Commands;

public class CreatePaymentTermsCommandHandler : IRequestHandler<CreatePaymentTermsCommand, object>
{
    public Task<object> Handle(CreatePaymentTermsCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
