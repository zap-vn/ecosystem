using MediatR;
using CRM.Customer.Application.Features.Customers.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Queries
{
    public class GetCustomerByIdQuery : IRequest<CustomerDto>
    {
        public string Id { get; set; }

        public GetCustomerByIdQuery(string id)
        {
            Id = id;
        }
    }
}

