using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Brands.v1.Commands;

public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, object>
{
    public async Task<object> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var id = Guid.NewGuid();
        return CrmResponse.Created(new { id });
    }
}
