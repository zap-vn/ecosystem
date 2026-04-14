using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.ModifierGroup.v1.Commands;

public class DeleteModifierGroupCommandHandler : IRequestHandler<DeleteModifierGroupCommand, object>
{
    public Task<object> Handle(DeleteModifierGroupCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
