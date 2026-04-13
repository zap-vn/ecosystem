using MediatR;
using CRM.Location.Application.Features.Locations.DTOs;
using CRM.BuildingBlocks.Models;
using System.Collections.Generic;
using System;

namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Queries
{
    public class GetLocationListQuery : IRequest<PagedResult<LocationDto>>
    {
        public LocationListRequestDto Request { get; set; } = new LocationListRequestDto();
    }
}


