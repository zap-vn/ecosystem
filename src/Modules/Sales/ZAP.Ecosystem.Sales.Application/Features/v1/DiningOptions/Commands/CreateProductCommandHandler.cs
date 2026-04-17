using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Sales.Application.Features.Sales.v1.DiningOptions.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, object>
{
    public Task<object> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}




