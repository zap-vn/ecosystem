using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.CRM.Application.Features.Customers.v1.Commands;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, object>
{
    public Task<object> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}




