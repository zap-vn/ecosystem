using MediatR;

namespace ZAP.Ecosystem.CRM.Application.Features.Customers.v1.Queries;

public class GetCustomerByIdQuery : IRequest<object>
{
    public Guid Id { get; set; }
    public GetCustomerByIdQuery(Guid id) => Id = id;
}




