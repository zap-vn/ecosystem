using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Categories.v1.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.Categories.v1.Queries;

public class GetCategoryListQuery : IRequest<object>
{
    public CategoryListRequestDto Request { get; set; } = new();
}
