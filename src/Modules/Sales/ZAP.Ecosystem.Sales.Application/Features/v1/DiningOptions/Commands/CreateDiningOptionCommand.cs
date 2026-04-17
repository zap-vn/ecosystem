using MediatR;

namespace ZAP.Ecosystem.Sales.Application.Features.Sales.v1.DiningOptions.Commands;
    public class CreateDiningOptionCommand : IRequest<object>
    {
        public string Code { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public int SortOrder { get; set; } = 0;
    }




