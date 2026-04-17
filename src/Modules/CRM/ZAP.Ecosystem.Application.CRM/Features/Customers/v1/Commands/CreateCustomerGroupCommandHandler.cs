using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.CRM.Application.Features.Customers.v1.Commands;

public class CreateCustomerGroupCommandHandler : IRequestHandler<CreateCustomerGroupCommand, object>
{
    public Task<object> Handle(CreateCustomerGroupCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}




