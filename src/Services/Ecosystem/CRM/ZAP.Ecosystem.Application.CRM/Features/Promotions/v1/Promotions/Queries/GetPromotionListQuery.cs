using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Promotions.v1.Queries;

public class GetPromotionListQuery : IRequest<object>
{
    public ZAP.Ecosystem.Application.CRM.Features.Promotions.v1.DTOs.PromotionListRequestDto Request { get; set; } = new();
}
