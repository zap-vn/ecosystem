using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.CRM.Application.Features.Customers.v1.Queries;

public class GetCustomerGroupByIdQueryHandler : IRequestHandler<GetCustomerGroupByIdQuery, object>
{
    public Task<object> Handle(GetCustomerGroupByIdQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}




