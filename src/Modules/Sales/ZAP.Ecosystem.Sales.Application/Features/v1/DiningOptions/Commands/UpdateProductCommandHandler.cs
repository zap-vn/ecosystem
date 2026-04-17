using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Sales.Application.Features.Sales.v1.DiningOptions.Commands;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, object>
{
    public Task<object> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}




