using CRM.Product.Application.Features.Products.DTOs;
using CRM.BuildingBlocks.Models;
using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.Queries
{
    public class GetBrandsQuery : IRequest<PagedResult<BrandDto>>
    {
        public BrandListRequestDto Request { get; set; } = new();
    }
}

