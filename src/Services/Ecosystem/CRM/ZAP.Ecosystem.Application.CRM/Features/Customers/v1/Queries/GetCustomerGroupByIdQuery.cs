using MediatR;
using CRM.Customer.Application.Features.CustomerGroups.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Queries
{
    public class GetCustomerGroupByIdQuery : IRequest<CustomerGroupDto>
    {
        public string Id { get; set; }

        public GetCustomerGroupByIdQuery(string id)
        {
            Id = id;
        }
    }
}

