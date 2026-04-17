using MediatR;

namespace ZAP.Ecosystem.Finance.Application.Features.v1.Queries;

public class GetPaymentTermsByIdQuery : IRequest<object>
{
    public string Id { get; set; }

    public GetPaymentTermsByIdQuery(string id)
    {
        Id = id;
    }
}




