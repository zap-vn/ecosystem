using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Units.v1.Commands;

public class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, object>
{
    public Task<object> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
