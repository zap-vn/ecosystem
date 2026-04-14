using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Queries;

public class GetPaymentTermsByIdQuery : IRequest<object>
{
    public string Id { get; set; }

    public GetPaymentTermsByIdQuery(string id)
    {
        Id = id;
    }
}
