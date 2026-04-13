using MediatR;
using System.Collections.Generic;
using CRM.Collection.Application.Features.Collections.DTOs;
using CRM.BuildingBlocks.Models;

namespace ZAP.Ecosystem.Application.CRM.Features.Collections.v1.Queries
{
    public class GetCollectionListQuery : IRequest<PagedResult<CollectionDto>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
    }
}


