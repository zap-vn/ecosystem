using MediatR;

namespace ZAP.Ecosystem.Finance.Application.Features.v1.Queries;

public class GetPaymentTypeListQuery : IRequest<object>
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? Search { get; set; }
}




