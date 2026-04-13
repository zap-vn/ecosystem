using MediatR;
using System.Collections.Generic;
using CRM.Location.Application.Features.Locations.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Queries
{
    public class GetProvinceListQuery : IRequest<List<ProvinceDto>>
    {
        public int LocaleId { get; set; } = 1;
    }
}


