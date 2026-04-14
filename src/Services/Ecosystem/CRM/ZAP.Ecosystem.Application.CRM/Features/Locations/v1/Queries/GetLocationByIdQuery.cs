using MediatR;
using System;

namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Queries;

public class GetLocationByIdQuery : IRequest<object>
{
    public Guid Id { get; set; }
}

