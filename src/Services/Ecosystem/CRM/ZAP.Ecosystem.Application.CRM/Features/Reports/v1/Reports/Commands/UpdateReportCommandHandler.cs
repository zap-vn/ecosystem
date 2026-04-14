using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Reports.v1.Commands;

public class UpdateReportCommandHandler : IRequestHandler<UpdateReportCommand, object>
{
    public Task<object> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(CrmResponse.Updated(new { id = request.Id }));
    }
}
