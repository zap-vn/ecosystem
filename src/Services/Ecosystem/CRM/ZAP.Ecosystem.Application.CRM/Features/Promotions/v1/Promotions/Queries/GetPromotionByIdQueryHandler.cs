using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Promotions.v1.Queries;

public class GetPromotionByIdQueryHandler : IRequestHandler<GetPromotionByIdQuery, object>
{
    public Task<object> Handle(GetPromotionByIdQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.NotFound("Promotion"));
}
