using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Brands.v1.DTOs;
using ZAP.Ecosystem.Shared.Data;

namespace ZAP.Ecosystem.Application.CRM.Features.Brands.v1.Queries
{
    public class GetBrandListQuery : IRequest<PagedResult<BrandDto>>
    {
        public BrandListRequestDto Request { get; set; } = new();
    }
}
