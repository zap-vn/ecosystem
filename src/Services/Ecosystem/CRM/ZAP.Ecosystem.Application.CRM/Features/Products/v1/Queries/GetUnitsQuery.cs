using CRM.Product.Application.Features.Products.DTOs;
using CRM.BuildingBlocks.Models;
using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.Queries
{
    public class GetUnitsQuery : IRequest<PagedResult<UnitDto>>
    {
        public UnitListRequestDto Request { get; set; } = new();
    }
}

