using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Finance.Application.Features.v1.Commands;

public class CreatePaymentTermsCommandHandler : IRequestHandler<CreatePaymentTermsCommand, object>
{
    public Task<object> Handle(CreatePaymentTermsCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}




