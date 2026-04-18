using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using ZAP.Ecosystem.Shared.Responses;

namespace ZAP.CRM.Catalog.Application.Features.Commons.Units;

public class GetUnitByIdQueryHandler(IUnitRepository repository) : IRequestHandler<GetUnitByIdQuery, object>
{
    public async Task<object> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
    {
        var unit = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (unit == null) return CrmResponse.NotFound("Unit");

        var dto = new UnitDto
        {
            id = unit.id,
            serial_id = unit.serial_number,
            code = unit.code,
            name = unit.name,
            symbol = unit.code,
            precision = unit.precision,
            is_active = unit.is_active,
            status = new DTOs.StatusDto
            {
                id = unit.is_active ? 9001 : 9002,
                name = unit.is_active ? "Active" : "Inactive",
                color = unit.is_active ? "#10b981" : "#64748b"
            }
        };

        return CrmResponse.Ok(dto);
    }
}
