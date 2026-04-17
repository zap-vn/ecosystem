using MediatR;

namespace ZAP.Ecosystem.CRM.Application.Features.Promotions.v1.Queries;

public class GetPromotionByIdQuery : IRequest<object>
{
    public string Id { get; set; }
    public GetPromotionByIdQuery(string id) => Id = id;
}




