using MediatR;
using CRM.Customer.Application.Features.Customers.DTOs;
using CRM.BuildingBlocks.Models;

namespace ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Queries
{
    public class GetCustomerListQuery : IRequest<PagedResult<CustomerListDto>>
    {
        public CustomerListRequestDto Request { get; set; } = new();
    }
}

