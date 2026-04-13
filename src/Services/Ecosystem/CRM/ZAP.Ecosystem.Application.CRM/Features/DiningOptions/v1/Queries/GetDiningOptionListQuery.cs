using MediatR;
using CRM.DiningOption.Application.Features.DiningOptions.DTOs;
using System.Collections.Generic;

namespace ZAP.Ecosystem.Application.CRM.Features.DiningOptions.v1.Queries
{
    public class GetDiningOptionListQuery : IRequest<IEnumerable<DiningOptionDto>>
    {
    }
}


