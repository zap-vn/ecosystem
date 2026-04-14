using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Reports.v1.Queries;

public class GetReportByIdQuery : IRequest<object>
{
    public string Id { get; set; }

    public GetReportByIdQuery(string id)
    {
        Id = id;
    }
}
