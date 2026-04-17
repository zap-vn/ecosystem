using MediatR;

namespace ZAP.Ecosystem.CRM.Application.Features.Promotions.v1.Queries;

public class GetPromotionListQuery : IRequest<object>
{
    public ZAP.Ecosystem.CRM.Application.Features.Promotions.v1.DTOs.PromotionListRequestDto Request { get; set; } = new();
}




