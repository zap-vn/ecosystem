using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Promotions.v1.Commands;

public class UpdatePromotionCommandHandler : IRequestHandler<UpdatePromotionCommand, object>
{
    public Task<object> Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
