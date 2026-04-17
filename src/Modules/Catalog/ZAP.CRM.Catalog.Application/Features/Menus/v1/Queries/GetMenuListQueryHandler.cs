using ZAP.CRM.Catalog.Domain.Interfaces.Brands;
using ZAP.CRM.Catalog.Domain.Interfaces.Products;
using ZAP.CRM.Catalog.Domain.Interfaces.Menus;
using ZAP.CRM.Catalog.Domain.Interfaces.Categories;
using ZAP.CRM.Catalog.Domain.Interfaces.Locations;
using ZAP.CRM.Catalog.Domain.Interfaces.Modifiers;
using ZAP.CRM.Catalog.Domain.Interfaces.Geography;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using ZAP.Ecosystem.Shared.Entities;
using ZAP.Ecosystem.Shared.Responses;
using ZAP.Ecosystem.Shared.Data;
using ZAP.Ecosystem.Shared.Interfaces;
using ZAP.CRM.Catalog.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Menus;
using ZAP.CRM.Catalog.Domain.Entities.Categories;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using ZAP.CRM.Catalog.Domain.Entities.Modifiers;
using ZAP.CRM.Catalog.Domain.Entities.Geography;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using ZAP.CRM.Catalog.Domain.Interfaces;
namespace ZAP.CRM.Catalog.Application.Features.Menus.v1.Queries;

public class GetMenuListQueryHandler : IRequestHandler<GetMenuListQuery, object>
{
    private readonly IMenuRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public GetMenuListQueryHandler(IMenuRepository repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<object> Handle(GetMenuListQuery request, CancellationToken cancellationToken)
    {
        var req = request.Request;
        Guid? tenantId = Guid.TryParse(_currentUserService.UserGuid, out var g) ? g : null;

        var (items, total) = await _repository.GetPagedAsync(
            req.PageIndex,
            req.PageSize,
            tenantId,
            req.Search,
            req.Filters?.IsActive,
            req.Filters?.MenuType,
            _currentUserService.LocaleId,
            req.Sort?.Field ?? "name",
            req.Sort?.Descending ?? false);

        var dtos = items.Select(x => new DTOs.MenuListResultDto
        {
            id           = x.id,
            serial_id    = x.serial_id,
            name         = x.name,
            menu_type    = x.menu_type,
            app_id       = x.app_id?.ToString(),
            status_id    = x.status_id,
            status_code  = x.status_code,
            status_name  = x.status_name,
            timezone_id  = x.timezone_id,
            is_active    = x.is_active,
            sections_count = x.sections.Count,
            total_items    = x.TotalItems
        }).ToList();

        return CrmResponse.Paged(new PagedResult<DTOs.MenuListResultDto>(dtos, total, req.PageIndex, req.PageSize));
    }
}



