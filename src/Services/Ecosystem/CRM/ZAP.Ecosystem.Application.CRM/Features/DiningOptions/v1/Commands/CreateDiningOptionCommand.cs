using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.DiningOptions.v1.Commands
{
    public class CreateDiningOptionCommand : IRequest<object>
    {
        public string Code { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public int SortOrder { get; set; } = 0;
    }
}


