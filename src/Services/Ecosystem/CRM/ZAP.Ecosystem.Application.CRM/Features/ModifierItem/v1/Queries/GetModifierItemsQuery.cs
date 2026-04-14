using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.ModifierItem.v1.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.ModifierItem.v1.Queries;

public class GetModifierItemsQuery : IRequest<object>
{
    public ModifierItemListRequestDto Request { get; set; } = new();
}
