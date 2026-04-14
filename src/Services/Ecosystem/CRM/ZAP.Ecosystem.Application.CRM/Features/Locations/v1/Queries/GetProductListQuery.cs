using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Locations.v1.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Queries;

// Renamed from GetProductListQuery to avoid conflict with Products.v1.Queries.GetProductListQuery
public class GetLocProductListQuery : IRequest<object>
{
    public LocProductListRequestDto Request { get; set; } = new();
}
