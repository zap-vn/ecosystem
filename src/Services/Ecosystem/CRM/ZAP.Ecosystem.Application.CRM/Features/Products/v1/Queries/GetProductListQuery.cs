using MediatR;



namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.Queries
{
    public class GetProductListQuery : IRequest<PagedResult<ProductDto>>
    {
        public ProductListRequestDto Request { get; set; } = new();
    }
}

