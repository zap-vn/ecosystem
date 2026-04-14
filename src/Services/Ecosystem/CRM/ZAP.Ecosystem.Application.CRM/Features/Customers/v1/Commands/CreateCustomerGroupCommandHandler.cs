using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Commands;

public class CreateCustomerGroupCommandHandler : IRequestHandler<CreateCustomerGroupCommand, object>
{
    public Task<object> Handle(CreateCustomerGroupCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
