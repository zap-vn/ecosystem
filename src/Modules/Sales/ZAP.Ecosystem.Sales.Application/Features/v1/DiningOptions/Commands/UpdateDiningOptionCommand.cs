using MediatR;

namespace ZAP.Ecosystem.Sales.Application.Features.Sales.v1.DiningOptions.Commands;

public class UpdateDiningOptionCommand : IRequest<object>
{
    public string Id { get; set; } = string.Empty;
}




