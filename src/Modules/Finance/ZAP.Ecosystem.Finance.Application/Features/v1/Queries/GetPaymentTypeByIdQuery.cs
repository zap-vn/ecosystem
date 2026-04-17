using MediatR;

namespace ZAP.Ecosystem.Finance.Application.Features.v1.Queries;

public class GetPaymentTypeByIdQuery : IRequest<object>
{
    public string Id { get; set; }

    public GetPaymentTypeByIdQuery(string id)
    {
        Id = id;
    }
}




