using MediatR;

namespace ZAP.Ecosystem.Finance.Application.Features.v1.Commands;
    public class CreatePaymentTypeCommand : IRequest<object>
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }




