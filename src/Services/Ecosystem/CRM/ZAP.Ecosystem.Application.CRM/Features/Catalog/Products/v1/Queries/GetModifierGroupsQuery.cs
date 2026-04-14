using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.ModifierGroup.v1.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.Queries;

public class GetModifierGroupsQuery : IRequest<object>
{
    public ModifierGroupListRequestDto Request { get; set; } = new();
}
