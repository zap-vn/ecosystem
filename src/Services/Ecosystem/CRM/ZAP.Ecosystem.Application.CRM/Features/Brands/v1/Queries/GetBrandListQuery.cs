using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Brands.v1.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.Brands.v1.Queries;

public class GetBrandListQuery : IRequest<object>
{
    public BrandListRequestDto Request { get; set; } = new();
}
