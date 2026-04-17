using ZAP.CRM.Catalog.Application.Features.Products.v1.DTOs;
using MediatR;

namespace ZAP.CRM.Catalog.Application.Features.Products.v1.Queries;

public class GetProductListQuery : IRequest<object>
{
    public ProductListRequestDto Request { get; set; } = new();
}



