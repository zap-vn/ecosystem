using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.CRM.Common;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Shared.Data;

namespace ZAP.Ecosystem.Application.CRM.Features.Promotions.v1.Queries;

public class GetPromotionListQueryHandler : IRequestHandler<GetPromotionListQuery, object>
{
    private readonly IPromotionRepository _repository;
    public GetPromotionListQueryHandler(IPromotionRepository repository) => _repository = repository;

    public async Task<object> Handle(GetPromotionListQuery query, CancellationToken cancellationToken)
    {
        var result = await _repository.GetPagedAsync(
            query.Request.PageIndex,
            query.Request.PageSize,
            statusId: query.Request.Filters?.StatusId,
            discountTypeId: query.Request.Filters?.DiscountTypeId,
            sortField: query.Request.Sort?.Field ?? "name",
            sortDescending: query.Request.Sort?.Descending ?? false);

        return CrmResponse.Paged(result, "OK");
    }
}
