using CRM.ModifierGroup.Application.Features.ModifierGroups.DTOs;
using CRM.BuildingBlocks.Models;
using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.ModifierGroup.v1.Queries
{
    public class GetModifierGroupsQuery : IRequest<PagedResult<ModifierGroupDto>>
    {
        public ModifierGroupListRequestDto Request { get; set; } = new();
    }
}

