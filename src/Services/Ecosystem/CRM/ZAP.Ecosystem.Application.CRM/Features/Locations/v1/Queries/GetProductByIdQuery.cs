using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Queries;

// Renamed from GetProductByIdQuery to avoid conflict with Products.v1.Queries.GetProductByIdQuery
public class GetLocProductByIdQuery : IRequest<object>
{
    public string Id { get; set; }

    public GetLocProductByIdQuery(string id)
    {
        Id = id;
    }
}
