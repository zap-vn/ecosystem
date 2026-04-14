using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Menu.v1.Commands;

public class MenuCommandHandlers : IRequestHandler<CreateMenuCommand, object>
{
    public Task<object> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
