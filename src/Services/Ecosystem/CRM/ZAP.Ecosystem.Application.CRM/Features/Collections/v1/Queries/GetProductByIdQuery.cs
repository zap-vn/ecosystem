using MediatR;
using CRM.Collection.Application.Features.Products.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.Collections.v1.Queries
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


