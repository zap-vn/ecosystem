using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.GeoCountry.v1.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.GeoCountry.v1.Queries
{
    public class GetGeoCountryListQuery : IRequest<object>
    {
        public GeoCountryListRequestDto Request { get; set; } = new();
    }
}
