using MediatR;
using CRM.Customer.Application.Features.CustomerGroups.DTOs;
using CRM.BuildingBlocks.Models;

namespace ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Queries
{
    public class GetCustomerGroupListQuery : IRequest<PagedResult<CustomerGroupDto>>
    {
        public FilterDTOs Filter { get; set; } = new();
    }
}

