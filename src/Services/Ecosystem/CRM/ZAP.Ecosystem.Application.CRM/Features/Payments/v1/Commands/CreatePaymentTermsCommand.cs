using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Commands
{
    public class CreatePaymentTermsCommand : IRequest<string>
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Days { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

