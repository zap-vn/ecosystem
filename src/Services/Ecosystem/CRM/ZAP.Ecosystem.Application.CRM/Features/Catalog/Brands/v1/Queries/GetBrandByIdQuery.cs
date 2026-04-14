using MediatR;
using System;

namespace ZAP.Ecosystem.Application.CRM.Features.Brands.v1.Queries;

public class GetBrandByIdQuery : IRequest<object>
{
    public Guid Id { get; set; }
}
