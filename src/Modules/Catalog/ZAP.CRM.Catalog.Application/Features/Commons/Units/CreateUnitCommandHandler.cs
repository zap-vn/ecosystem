using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using ZAP.Ecosystem.Shared.Responses;

namespace ZAP.CRM.Catalog.Application.Features.Commons.Units;

public class CreateUnitCommandHandler(IUnitRepository repository) : IRequestHandler<CreateUnitCommand, object>
{
    public async Task<object> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        int nextId = await repository.GetMaxIdAsync() + 1;
        
        var unit = new UomItem
        {
            id = nextId,
            code = request.code,
            name = request.name,
            precision = request.precision,
            is_active = request.is_active
        };

        await repository.AddAsync(unit, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);

        return CrmResponse.Created(new { unit.id }, "Unit created successfully");
    }
}
