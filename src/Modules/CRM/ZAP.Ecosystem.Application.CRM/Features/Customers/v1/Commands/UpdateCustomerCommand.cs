using MediatR;

namespace ZAP.Ecosystem.CRM.Application.Features.Customers.v1.Commands;
    public class UpdateCustomerCommand : IRequest<object>
    {
        public string Id { get; set; } = string.Empty;
        public string BusinessName { get; set; } = string.Empty;
        public string MerchantName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string CustomerCode { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;
    }




