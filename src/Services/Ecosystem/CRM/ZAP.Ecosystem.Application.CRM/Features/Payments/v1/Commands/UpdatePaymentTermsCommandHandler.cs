using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Commands;

public class UpdatePaymentTermsCommandHandler : IRequestHandler<UpdatePaymentTermsCommand, object>
{
    public Task<object> Handle(UpdatePaymentTermsCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
