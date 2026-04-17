using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Sales.Application.Features.Sales.v1.DiningOptions.Commands;

public class CreateDiningOptionCommandHandler : IRequestHandler<CreateDiningOptionCommand, object>
{
    public Task<object> Handle(CreateDiningOptionCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}




