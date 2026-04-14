using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Promotions.v1.Commands;

public class CreatePromotionCommandHandler : IRequestHandler<CreatePromotionCommand, object>
{
    public Task<object> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
