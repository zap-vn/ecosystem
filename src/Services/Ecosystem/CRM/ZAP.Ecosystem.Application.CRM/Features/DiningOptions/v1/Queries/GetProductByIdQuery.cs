using MediatR;
using CRM.DiningOption.Application.Features.Products.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.DiningOptions.v1.Queries
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


