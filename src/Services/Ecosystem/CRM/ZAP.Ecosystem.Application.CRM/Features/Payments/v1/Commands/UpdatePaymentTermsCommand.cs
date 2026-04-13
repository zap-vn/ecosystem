using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Commands
{
    public class UpdatePaymentTermsCommand : IRequest<bool>
    {
        public string Id { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Days { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

