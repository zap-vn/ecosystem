using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.ModifierGroup.v1.DTOs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.ModifierGroup.v1.Queries;

public class GetModifierGroupsQueryHandler : IRequestHandler<GetModifierGroupsQuery, object>
{
    private readonly IModifierGroupRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public GetModifierGroupsQueryHandler(IModifierGroupRepository repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<object> Handle(GetModifierGroupsQuery request, CancellationToken cancellationToken)
    {
        Guid? tenantId = Guid.TryParse(_currentUserService.UserGuid, out var g) ? g : null;

        var (items, total) = await _repository.GetPagedAsync(
            request.Request.PageIndex,
            request.Request.PageSize,
            tenantId,
            request.Request.Search,
            request.Request.Filters?.StatusId,
            request.Request.Filters?.DisplayType,
            request.Request.Sort?.Field ?? "name",
            request.Request.Sort?.Descending ?? false);

        var dtos = items.Select(x => new ModifierGroupDto
        {
            id = x.id,
            serial_id = x.serial_id,
            tenant_id = x.tenant_id,
            name = x.name,
            min_selection = x.min_selection,
            max_selection = x.max_selection,
            is_required = x.is_required,
            sort_order = x.sort_order,
            status_id = x.status_id,
            status_code = x.status?.code
        }).ToList();

        return CrmResponse.Paged(new PagedResult<ModifierGroupDto>(dtos, total, request.Request.PageIndex, request.Request.PageSize));
    }
}
