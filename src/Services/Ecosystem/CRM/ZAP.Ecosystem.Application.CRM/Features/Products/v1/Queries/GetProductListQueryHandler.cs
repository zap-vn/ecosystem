using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Products.v1.DTOs;
using ProductDto = ZAP.Ecosystem.Application.CRM.Features.Products.v1.DTOs.ProductDto;
using ProductVariantDto = ZAP.Ecosystem.Application.CRM.Features.Products.v1.DTOs.ProductVariantDto;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.Queries;

public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, object>
{
    private readonly IProductRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public GetProductListQueryHandler(IProductRepository repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<object> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var req = request.Request;
        Guid? tenantId = Guid.TryParse(_currentUserService.UserGuid, out var tg) ? tg : null;
        Guid? categoryId = Guid.TryParse(req.Filters?.CategoryId, out var cg) ? cg : null;
        Guid? locationId = Guid.TryParse(req.Filters?.LocationId, out var lg) ? lg : null;
        int localeId = req.Filters?.LocaleId ?? _currentUserService.LocaleId;

        var (products, total) = await _repository.GetPagedProductsAsync(
            req.Page,
            req.PageSize,
            tenantId,
            req.Search,
            req.Filters?.StatusId,
            categoryId,
            locationId,
            localeId,
            req.Filters?.ProductTypeId,
            req.Sort?.Field ?? "created_at",
            req.Sort?.Descending ?? true);

        var dtos = products.Select(p =>
        {
            var primaryCategory = p.category_mappings.FirstOrDefault(cm => cm.is_primary) ?? p.category_mappings.FirstOrDefault();
            var statusText = p.status?.translations?.FirstOrDefault(t => t.locale_id == localeId)?.name ?? p.status?.code ?? "";

            var productVariants = p.variants.Select(v =>
            {
                var primaryMedia = v.media.FirstOrDefault(m => m.is_primary) ?? v.media.FirstOrDefault();
                var inventoryItems = locationId.HasValue
                    ? v.inventory_items.Where(i => i.location_id == locationId).ToList()
                    : v.inventory_items;
                var locationPrice = v.location_pricing.FirstOrDefault(lp => !locationId.HasValue || lp.location_id == locationId);
                var price = locationPrice?.sale_price_override ?? v.sale_price ?? v.base_price;

                return new ProductVariantDto
                {
                    id = v.id,
                    serial_id = v.serial_id,
                    sku_code = v.sku_code,
                    barcode = v.barcode,
                    variant_name = v.variant_name ?? "",
                    base_price = v.base_price,
                    sale_price = price,
                    cost_price = v.cost_price,
                    is_active = true,
                    media_url = primaryMedia?.media_url,
                    qty_on_hand = inventoryItems.Sum(i => i.qty_on_hand),
                    location_count = inventoryItems.Select(i => i.location_id).Distinct().Count()
                };
            }).ToList();

            var lead = productVariants.FirstOrDefault();
            return new ProductDto
            {
                id = p.id,
                serial_id = p.serial_id,
                product_id = p.id,
                name = p.name,
                tenant_id = p.tenant_id,
                product_type_id = p.product_type_id,
                product_type_text = p.product_type?.translations?.FirstOrDefault(t => t.locale_id == localeId)?.name ?? p.product_type?.code ?? "",
                status_id = p.status_id,
                status_name = statusText,
                status_code = p.status?.code ?? "",
                category_id = primaryCategory?.category_id,
                category_name = primaryCategory?.category?.name,
                media_url = lead?.media_url,
                variant_name = lead?.variant_name,
                sku_code = lead?.sku_code,
                barcode = lead?.barcode,
                sale_price = lead?.sale_price,
                qty_on_hand = productVariants.Sum(v => v.qty_on_hand),
                created_at = p.created_at,
                updated_at = p.updated_at,
                variants = productVariants
            };
        }).ToList();

        return CrmResponse.Paged(new PagedResult<ProductDto>(dtos, total, req.Page, req.PageSize));
    }
}

