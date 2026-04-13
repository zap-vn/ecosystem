using CRM.Category.Application.Features.Categories.DTOs;
using CRM.BuildingBlocks.Models;
using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Categories.v1.Queries
{
    public class GetCategoryListQuery : IRequest<PagedResult<CategoryDto>>
    {
        public CategoryListRequestDto Request { get; set; } = new();
    }
}

