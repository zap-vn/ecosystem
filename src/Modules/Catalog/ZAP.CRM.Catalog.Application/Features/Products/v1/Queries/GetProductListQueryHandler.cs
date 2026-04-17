using ZAP.CRM.Catalog.Application.Features.Products.v1.DTOs;
using ZAP.Ecosystem.Shared.Responses;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;
using ZAP.CRM.Catalog.Domain.Interfaces.Products;

namespace ZAP.CRM.Catalog.Application.Features.Products.v1.Queries;

public class GetProductListQueryHandler(IProductRepository repository, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetProductListQuery, object>
{
    public async Task<object> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var localeId = httpContextAccessor.HttpContext?.Items["LocaleId"] as int? ?? 2;
        var req = request.Request;
        var (items, total) = await repository.GetPagedProductsAsync(
            req.PageIndex,
            req.PageSize,
            null, // tenantId (can be filled later from context)
            req.Search,
            req.Filters?.StatusId,
            Guid.TryParse(req.Filters?.CategoryId, out var catId) ? catId : null,
            Guid.TryParse(req.Filters?.LocationId, out var locId) ? locId : null,
            localeId,
            req.Filters?.ProductTypeId);

        var dtos = items.Select(x => new ProductDto
        {
            id = x.id,
            name = x.translations?.FirstOrDefault(t => t.locale_id == localeId)?.Name 
                   ?? x.translations?.FirstOrDefault()?.Name 
                   ?? "Unknown Product",
            short_description = x.translations?.FirstOrDefault(t => t.locale_id == localeId)?.ShortDescription,
            serial_id = x.serial_id,
            tenant_id = x.tenant_id,
            brand_id = x.brand_id,
            category_id = x.category_mappings.FirstOrDefault()?.category_id,
            category_name = x.category?.translations?.FirstOrDefault(t => t.locale_id == localeId)?.Name 
                            ?? x.category?.translations?.FirstOrDefault()?.Name 
                            ?? "Uncategorized",
            status_id = x.status_id,
            status_name = x.status?.translations?.FirstOrDefault(t => t.locale_id == localeId)?.name 
                          ?? x.status?.translations?.FirstOrDefault()?.name,
            created_at = x.created_at
        }).ToList();

        return CrmResponse.Paged(new ZAP.Ecosystem.Shared.Data.PagedResult<ProductDto>(dtos, total, req.PageIndex, req.PageSize));
    }
}
