using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Collections.v1.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, object>
{
    public Task<object> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
