using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.DiningOptions.v1.Queries;

public class GetDiningOptionByIdQuery : IRequest<object>
{
    public string Id { get; set; } = string.Empty;
}
