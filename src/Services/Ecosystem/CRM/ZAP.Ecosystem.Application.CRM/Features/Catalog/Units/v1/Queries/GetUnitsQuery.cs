using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Units.v1.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.Units.v1.Queries;

public class GetUnitsQuery : IRequest<object>
{
    public UnitListRequestDto Request { get; set; } = new();
}
