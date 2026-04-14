using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Promotion.Application.Features.Promotions.Commands;

public class UpdatePromotionCommandHandler : IRequestHandler<UpdatePromotionCommand, object>
{
    public Task<object> Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
