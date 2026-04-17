using ZAP.CRM.Catalog.Domain.Interfaces.Brands;
using ZAP.CRM.Catalog.Domain.Interfaces.Products;
using ZAP.CRM.Catalog.Domain.Interfaces.Menus;
using ZAP.CRM.Catalog.Domain.Interfaces.Categories;
using ZAP.CRM.Catalog.Domain.Interfaces.Locations;
using ZAP.CRM.Catalog.Domain.Interfaces.Modifiers;
using ZAP.CRM.Catalog.Domain.Interfaces.Geography;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using ZAP.Ecosystem.Shared.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZAP.Ecosystem.Shared.Responses;
using ZAP.CRM.Catalog.Application.Features.Modifiers.v1.DTOs;
using ZAP.CRM.Catalog.Domain;
using ZAP.Ecosystem.Shared.Data;

using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Menus;
using ZAP.CRM.Catalog.Domain.Entities.Categories;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using ZAP.CRM.Catalog.Domain.Entities.Modifiers;
using ZAP.CRM.Catalog.Domain.Entities.Geography;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using ZAP.CRM.Catalog.Domain.Interfaces;
namespace ZAP.CRM.Catalog.Application.Features.Modifiers.v1.Queries;

public class GetModifierItemsQueryHandler : IRequestHandler<GetModifierItemsQuery, object>
{
    private readonly IModifierItemRepository _repository;

    public GetModifierItemsQueryHandler(IModifierItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<object> Handle(GetModifierItemsQuery request, CancellationToken cancellationToken)
    {
        var (items, total) = await _repository.GetPagedAsync(
            request.Request.PageIndex,
            request.Request.PageSize,
            request.Request.Filters?.GroupId,
            request.Request.Filters?.StatusId,
            request.Request.Sort?.Field ?? "sort_order",
            request.Request.Sort?.Descending ?? false);

        var dtos = items.Select(x => new ModifierItemDto
        {
            id                 = x.id,
            serial_id          = x.serial_id,
            serial_number      = x.serial_number,
            group_id           = x.group_id,
            product_variant_id = x.product_variant_id,
            image_url          = x.image_url,
            price_override     = x.price_override,
            sort_order         = x.sort_order,
            status_id          = x.status_id,
            created_at         = x.created_at,
            updated_at         = x.updated_at,
        }).ToList();

        return CrmResponse.Paged(new PagedResult<ModifierItemDto>(dtos, total, request.Request.PageIndex, request.Request.PageSize));
    }
}



