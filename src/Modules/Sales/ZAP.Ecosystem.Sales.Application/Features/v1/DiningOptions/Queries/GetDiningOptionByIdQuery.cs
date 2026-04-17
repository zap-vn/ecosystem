using MediatR;

namespace ZAP.Ecosystem.Sales.Application.Features.Sales.v1.DiningOptions.Queries;

public class GetDiningOptionByIdQuery : IRequest<object>
{
    public string Id { get; set; } = string.Empty;
}




