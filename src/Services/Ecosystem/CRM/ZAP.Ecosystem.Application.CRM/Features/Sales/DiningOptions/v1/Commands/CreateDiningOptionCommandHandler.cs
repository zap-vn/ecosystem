using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.DiningOptions.v1.Commands;

public class CreateDiningOptionCommandHandler : IRequestHandler<CreateDiningOptionCommand, object>
{
    public Task<object> Handle(CreateDiningOptionCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
