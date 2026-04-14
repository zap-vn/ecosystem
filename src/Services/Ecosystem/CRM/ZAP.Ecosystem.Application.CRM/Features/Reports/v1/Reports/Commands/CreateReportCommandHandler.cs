using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Reports.v1.Commands;

public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, object>
{
    public Task<object> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(CrmResponse.Created(new { id = Guid.NewGuid() }));
    }
}
