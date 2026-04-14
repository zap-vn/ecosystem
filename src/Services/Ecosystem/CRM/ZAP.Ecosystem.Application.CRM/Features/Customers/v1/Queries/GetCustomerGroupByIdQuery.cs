using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Queries;

public class GetCustomerGroupByIdQuery : IRequest<object>
{
    public string Id { get; set; }
    public GetCustomerGroupByIdQuery(string id) => Id = id;
}
