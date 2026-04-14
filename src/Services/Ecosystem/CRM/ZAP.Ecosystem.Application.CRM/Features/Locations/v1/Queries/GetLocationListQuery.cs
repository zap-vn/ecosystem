using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Locations.v1.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Queries;

public class GetLocationListQuery : IRequest<object>
{
    public LocationListRequestDto Request { get; set; } = new();
    public string? AcceptLanguage { get; set; }
}
