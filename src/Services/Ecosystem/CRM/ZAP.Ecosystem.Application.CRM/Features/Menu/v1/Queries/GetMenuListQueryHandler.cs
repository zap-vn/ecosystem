using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using ZAP.Ecosystem.Shared.Data;
using ZAP.Ecosystem.Application.CRM.Common;

namespace ZAP.Ecosystem.Application.CRM.Features.Menu.v1.Queries;

public class GetMenuListQueryHandler : IRequestHandler<GetMenuListQuery, object>
{
    private readonly ZAP.Ecosystem.Domain.CRM.IMenuRepository _repository;
    private readonly ZAP.Ecosystem.Shared.Interfaces.ICurrentUserService _currentUserService;

    public GetMenuListQueryHandler(ZAP.Ecosystem.Domain.CRM.IMenuRepository repository, ZAP.Ecosystem.Shared.Interfaces.ICurrentUserService currentUserService)
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
