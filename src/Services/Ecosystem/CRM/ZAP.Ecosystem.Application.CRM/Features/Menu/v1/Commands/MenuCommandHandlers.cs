using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Menu.v1.Commands;

public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, object>
{
    public Task<object> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Created(new { id = Guid.NewGuid() }));
}

public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, object>
{
    public Task<object> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Updated(new { id = request.id }));
}

public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, object>
{
    public Task<object> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(new { id = request.id }));
}
