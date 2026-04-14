using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Brands.v1.Commands;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, object>
{
    public async Task<object> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return CrmResponse.Updated(new { id = request.Id });
    }
}
