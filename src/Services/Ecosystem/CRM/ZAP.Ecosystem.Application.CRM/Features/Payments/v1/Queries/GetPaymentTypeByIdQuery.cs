using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Queries;

public class GetPaymentTypeByIdQuery : IRequest<object>
{
    public string Id { get; set; }

    public GetPaymentTypeByIdQuery(string id)
    {
        Id = id;
    }
}
