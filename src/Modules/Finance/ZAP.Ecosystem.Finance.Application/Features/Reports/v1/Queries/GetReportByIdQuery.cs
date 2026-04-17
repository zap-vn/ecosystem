using MediatR;

namespace ZAP.Ecosystem.Finance.Application.Features.Reports.v1.Queries;

public class GetReportByIdQuery : IRequest<object>
{
    public string Id { get; set; }

    public GetReportByIdQuery(string id)
    {
        Id = id;
    }
}




