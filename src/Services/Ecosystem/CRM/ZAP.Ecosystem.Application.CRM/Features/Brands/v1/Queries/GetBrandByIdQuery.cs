using MediatR;
using CRM.Brand.Application.Features.Products.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.Brands.v1.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public string Id { get; set; }

        public GetProductByIdQuery(string id)
        {
            Id = id;
        }
    }
}

