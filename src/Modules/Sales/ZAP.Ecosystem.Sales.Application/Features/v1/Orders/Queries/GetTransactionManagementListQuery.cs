using MediatR;
using System;

namespace ZAP.Ecosystem.Sales.Application.Features.Sales.v1.Orders.Queries;

public class GetTransactionManagementListQuery : IRequest<object>
{
    public Guid? TenantId { get; set; }
    public string? OrderNumber { get; set; }
    public int? StatusId { get; set; }
    public string? Channel { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}




