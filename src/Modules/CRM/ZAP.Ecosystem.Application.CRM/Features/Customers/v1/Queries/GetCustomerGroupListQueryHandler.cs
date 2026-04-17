using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.CRM.Application.Features.Customers.v1.Queries;

public class GetCustomerGroupListQueryHandler : IRequestHandler<GetCustomerGroupListQuery, object>
{
    public Task<object> Handle(GetCustomerGroupListQuery request, CancellationToken cancellationToken)
        => Task.FromResult(CrmResponse.Ok(null));
}




