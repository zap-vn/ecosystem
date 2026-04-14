using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.DiningOptions.v1.Commands;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, object>
{
    public Task<object> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
