using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Categories.v1.Commands;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, object>
{
    public Task<object> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
