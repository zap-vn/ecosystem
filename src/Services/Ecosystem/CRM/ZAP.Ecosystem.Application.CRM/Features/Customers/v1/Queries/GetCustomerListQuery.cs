using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Customers.v1.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Queries;

public class GetCustomerListQuery : IRequest<object>
{
    public CustomerListRequestDto Request { get; set; } = new();
}
