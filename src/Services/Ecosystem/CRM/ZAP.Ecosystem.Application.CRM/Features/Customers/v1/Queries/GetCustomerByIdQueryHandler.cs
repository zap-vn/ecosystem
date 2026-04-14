using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Queries;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, object>
{
    public Task<object> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}
