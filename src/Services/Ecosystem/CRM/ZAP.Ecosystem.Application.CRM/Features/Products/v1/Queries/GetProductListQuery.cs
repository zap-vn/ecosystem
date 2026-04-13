using MediatR;
using CRM.Product.Application.Features.Products.DTOs;
using CRM.BuildingBlocks.Models;

namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.Queries
{
    public class GetProductListQuery : IRequest<PagedResult<ProductDto>>
    {
        public ProductListRequestDto Request { get; set; } = new();
    }
}

