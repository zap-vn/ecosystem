using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Reports.v1.Commands;

public class CreateReportCommand : IRequest<object>
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string ConfigurationJson { get; set; } = string.Empty;
}
