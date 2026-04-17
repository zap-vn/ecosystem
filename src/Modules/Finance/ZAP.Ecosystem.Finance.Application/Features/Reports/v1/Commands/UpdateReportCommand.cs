using MediatR;

namespace ZAP.Ecosystem.Finance.Application.Features.Reports.v1.Commands;

public class UpdateReportCommand : IRequest<object>
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string ConfigurationJson { get; set; } = string.Empty;
}




