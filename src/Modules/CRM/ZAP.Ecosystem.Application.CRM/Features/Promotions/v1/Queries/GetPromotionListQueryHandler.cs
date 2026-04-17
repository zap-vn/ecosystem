using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using System.Linq;

namespace ZAP.Ecosystem.CRM.Application.Features.Promotions.v1.Queries;

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

        var dtos = result.Items.Select(x => new DTOs.PromotionDto
        {
            id                 = x.id,
            serial_id          = x.serial_id,
            tenant_id          = x.tenant_id ?? Guid.Empty,
            legacy_id          = x.legacy_id,
            name               = x.name,
            short_name         = x.short_name,
            description        = x.description,
            is_automatic       = x.is_automatic,
            discount_value     = x.discount_value,
            status_id          = x.status_id,
            status_code        = x.status_code,
            status_name        = x.status_name,
            created_at         = x.created_at,
            updated_at         = x.updated_at ?? DateTime.UtcNow
        }).ToList();

        return CrmResponse.Paged(new PagedResult<DTOs.PromotionDto>(dtos, result.TotalCount, result.PageIndex, result.PageSize));
    }
}




