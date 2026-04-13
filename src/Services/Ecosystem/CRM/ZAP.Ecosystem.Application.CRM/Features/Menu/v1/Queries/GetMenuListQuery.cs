using MediatR;
using CRM.BuildingBlocks.Models;
using CRM.Menu.Application.Features.Menus.DTOs;
using System;

namespace ZAP.Ecosystem.Application.CRM.Features.Menu.v1.Queries
{
    public class GetMenuListQuery : IRequest<PagedResult<MenuListResultDto>>
    {
        public Guid? TenantId { get; set; }
        public MenuListRequestDto Request { get; set; } = new();
    }
}


