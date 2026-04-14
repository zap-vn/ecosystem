using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Queries;

public class GetCustomerByIdQuery : IRequest<object>
{
    public string Id { get; set; }
    public GetCustomerByIdQuery(string id) => Id = id;
}
