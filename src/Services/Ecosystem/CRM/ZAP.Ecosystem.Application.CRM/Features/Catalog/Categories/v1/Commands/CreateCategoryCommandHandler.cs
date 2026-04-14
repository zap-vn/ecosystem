using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Categories.v1.Commands;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, object>
{
    public Task<object> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
