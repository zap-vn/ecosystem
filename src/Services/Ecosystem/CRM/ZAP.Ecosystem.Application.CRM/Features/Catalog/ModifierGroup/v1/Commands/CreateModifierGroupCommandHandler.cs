using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.ModifierGroup.v1.Commands;

public class CreateModifierGroupCommandHandler : IRequestHandler<CreateModifierGroupCommand, object>
{
    public Task<object> Handle(CreateModifierGroupCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
