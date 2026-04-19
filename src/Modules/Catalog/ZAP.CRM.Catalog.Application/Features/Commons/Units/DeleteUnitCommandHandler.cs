using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using ZAP.Ecosystem.Shared.Responses;

namespace ZAP.CRM.Catalog.Application.Features.Commons.Units;

public class DeleteUnitCommandHandler(IUnitRepository repository) : IRequestHandler<DeleteUnitCommand, object>
{
    public async Task<object> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
    {
        var unit = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (unit == null) return CrmResponse.NotFound("Unit");

        await repository.DeleteAsync(unit, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);

        return CrmResponse.Ok(null, "Unit deleted successfully");
    }
}
