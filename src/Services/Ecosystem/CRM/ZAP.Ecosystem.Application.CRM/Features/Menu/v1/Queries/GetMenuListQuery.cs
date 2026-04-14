using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Menu.v1.Queries;

public class GetMenuListQuery : IRequest<object>
{
    public ZAP.Ecosystem.Application.CRM.Features.Menu.v1.DTOs.MenuListRequestDto Request { get; set; } = new();
}
