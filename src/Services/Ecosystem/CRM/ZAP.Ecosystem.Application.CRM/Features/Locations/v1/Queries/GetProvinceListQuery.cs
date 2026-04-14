using MediatR;
using System.Collections.Generic;

namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Queries;

public class GetProvinceListQuery : IRequest<object>
{
    public int LocaleId { get; set; } = 2;
}
