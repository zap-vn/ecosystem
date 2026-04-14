using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Products.v1.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.Queries;

public class GetProductListQuery : IRequest<object>
{
    public ProductListRequestDto Request { get; set; } = new();
}
