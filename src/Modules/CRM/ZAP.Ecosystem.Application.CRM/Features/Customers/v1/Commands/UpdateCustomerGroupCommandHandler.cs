using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.CRM.Application.Features.Customers.v1.Commands;

public class UpdateCustomerGroupCommandHandler : IRequestHandler<UpdateCustomerGroupCommand, object>
{
    public Task<object> Handle(UpdateCustomerGroupCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}




