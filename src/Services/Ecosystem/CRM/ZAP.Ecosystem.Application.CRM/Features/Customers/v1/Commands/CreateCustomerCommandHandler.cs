using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Commands;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, object>
{
    public Task<object> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
