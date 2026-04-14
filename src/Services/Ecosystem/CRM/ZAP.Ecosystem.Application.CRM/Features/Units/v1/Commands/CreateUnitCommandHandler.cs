using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Units.v1.Commands;

public class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand, object>
{
    public Task<object> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
