using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.ModifierGroup.v1.Commands;

public class UpdateModifierGroupCommandHandler : IRequestHandler<UpdateModifierGroupCommand, object>
{
    public Task<object> Handle(UpdateModifierGroupCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
