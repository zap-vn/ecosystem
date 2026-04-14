using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Commands;

public class UpdatePaymentTypeCommandHandler : IRequestHandler<UpdatePaymentTypeCommand, object>
{
    public Task<object> Handle(UpdatePaymentTypeCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
