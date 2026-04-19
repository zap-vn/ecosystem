using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using ZAP.Ecosystem.Shared.Responses;
using ZAP.Ecosystem.Shared.Interfaces;
using ZAP.Ecosystem.Shared.Data;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using System.Collections.Generic;
using System;

namespace ZAP.CRM.Catalog.Application.Features.Commons.Units;

public class GetUnitsListQueryHandler : IRequestHandler<GetUnitsListQuery, object>
{
    private readonly IUnitRepository _unitRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetUnitsListQueryHandler(IUnitRepository unitRepository, ICurrentUserService currentUserService)
    {
        _unitRepository = unitRepository;
        _currentUserService = currentUserService;
    }

    public async Task<object> Handle(GetUnitsListQuery request, CancellationToken cancellationToken)
    {
        Guid? tenantGuid = Guid.TryParse(_currentUserService.TenantId, out var parsedGuid) ? parsedGuid : null;
        var resultPaged = await _unitRepository.GetPagedAsync(
            request.page_index,
            request.page_size,
            tenantGuid,
            request.search,
            request.status_id,
            request.precision
        );

        IEnumerable<UomItem> items = resultPaged.Items;
        int total = resultPaged.Total;

        var dtos = items.Select(x => new UnitDto
        {
            id = x.id,
            serial_id = x.serial_number,
            code = x.code,
            name = x.name,
            symbol = x.code, 
            precision = x.precision,
            is_active = x.is_active,
            status = new DTOs.StatusDto
            {
                id = x.is_active ? 9001 : 9002,
                name = x.is_active ? "Active" : "Inactive",
                color = x.is_active ? "#10b981" : "#64748b"
            }
        }).ToList();

        var pagedResult = new PagedResult<UnitDto>(dtos, total, request.page_index, request.page_size);
        return CrmResponse.Paged(pagedResult);
    }
}
