using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Commands
{
    public class CreatePaymentTypeCommand : IRequest<string>
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}

