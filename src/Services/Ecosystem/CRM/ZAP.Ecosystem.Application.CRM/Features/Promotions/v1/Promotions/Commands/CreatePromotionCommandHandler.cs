using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Promotion.Application.Features.Promotions.Commands;

public class CreatePromotionCommandHandler : IRequestHandler<CreatePromotionCommand, object>
{
    public Task<object> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
