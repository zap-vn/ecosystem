

using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.Queries
{
    public class GetCategoryListQuery : IRequest<PagedResult<CategoryDto>>
    {
        public CategoryListRequestDto Request { get; set; } = new();
    }
}

