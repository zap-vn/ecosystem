using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using ZAP.Ecosystem.Shared.Responses;

namespace ZAP.CRM.Catalog.Application.Features.Commons.Units;

public class UpdateUnitCommandHandler(IUnitRepository repository) : IRequestHandler<UpdateUnitCommand, object>
{
    public async Task<object> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
    {
        var unit = await repository.GetByIdAsync(request.id, cancellationToken);
        if (unit == null) return CrmResponse.NotFound("Unit");

        unit.code = request.code;
        unit.name = request.name;
        unit.precision = request.precision;
        unit.is_active = request.is_active;

        await repository.UpdateAsync(unit, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);

        return CrmResponse.Updated(null, "Unit updated successfully");
    }
}
