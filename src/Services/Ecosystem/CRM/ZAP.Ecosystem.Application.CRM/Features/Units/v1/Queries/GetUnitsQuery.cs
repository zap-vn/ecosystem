using CRM.Unit.Application.Features.Units.DTOs;
using CRM.BuildingBlocks.Models;
using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Units.v1.Queries
{
    public class GetUnitsQuery : IRequest<PagedResult<UnitDto>>
    {
        public UnitListRequestDto Request { get; set; } = new();
    }
}

