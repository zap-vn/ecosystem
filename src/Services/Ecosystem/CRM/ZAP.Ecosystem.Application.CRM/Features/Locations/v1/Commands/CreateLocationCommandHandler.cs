using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Commands;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, object>
{
    public Task<object> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
