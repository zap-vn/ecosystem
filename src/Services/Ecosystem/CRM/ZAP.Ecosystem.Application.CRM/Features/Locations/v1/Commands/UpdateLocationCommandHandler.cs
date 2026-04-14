using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Commands;

public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, object>
{
    public Task<object> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
