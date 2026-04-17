using MediatR;

namespace ZAP.Ecosystem.CRM.Application.Features.Customers.v1.Queries;

public class GetCustomerListQuery : IRequest<object>
{
    public CustomerListRequestDto Request { get; set; } = new();
    public GetCustomerListQuery() {}
    public GetCustomerListQuery(CustomerListRequestDto request) => Request = request;
}




