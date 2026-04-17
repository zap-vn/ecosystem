using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Finance.Application.Features.v1.Commands;

public class CreatePaymentTypeCommandHandler : IRequestHandler<CreatePaymentTypeCommand, object>
{
    public Task<object> Handle(CreatePaymentTypeCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}




