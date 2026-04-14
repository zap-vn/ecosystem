using MediatR;
using System;

namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.Queries;

public class GetPriceListQuery : IRequest<object>
{
    public Guid? TenantId { get; set; }
    public Guid LocationId { get; set; }
    public Guid? CategoryId { get; set; }
    public string? SearchTerm { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
