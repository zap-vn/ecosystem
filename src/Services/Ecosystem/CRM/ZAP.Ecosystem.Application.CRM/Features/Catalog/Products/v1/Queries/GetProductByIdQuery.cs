using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.Queries;

public class GetProductByIdQuery : IRequest<object>
{
    public string Id { get; set; }

    public GetProductByIdQuery(string id)
    {
        Id = id;
    }
}
