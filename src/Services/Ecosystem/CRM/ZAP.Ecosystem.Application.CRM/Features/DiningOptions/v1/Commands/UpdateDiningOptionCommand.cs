using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.DiningOptions.v1.Commands;

public class UpdateDiningOptionCommand : IRequest<object>
{
    public string Id { get; set; } = string.Empty;
}
