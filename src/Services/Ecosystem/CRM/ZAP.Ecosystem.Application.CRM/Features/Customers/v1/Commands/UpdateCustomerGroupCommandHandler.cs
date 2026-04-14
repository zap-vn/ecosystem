using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Commands;

public class UpdateCustomerGroupCommandHandler : IRequestHandler<UpdateCustomerGroupCommand, object>
{
    public Task<object> Handle(UpdateCustomerGroupCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
