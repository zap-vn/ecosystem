using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Promotions.v1.Queries;

public class GetPromotionListQueryHandler : IRequestHandler<GetPromotionListQuery, object>
{
    public Task<object> Handle(GetPromotionListQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Paged(Array.Empty<object>(), 0, request.Request.PageIndex, request.Request.PageSize));
}
