using MediatR;
using CRM.Location.Application.Features.Locations.DTOs;
using System;

namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Queries
{
    public class GetLocationByIdQuery : IRequest<LocationDto?>
    {
        public Guid Id { get; set; }
        public int LocaleId { get; set; } = 2; // Default to 2 (VI)
    }

}


