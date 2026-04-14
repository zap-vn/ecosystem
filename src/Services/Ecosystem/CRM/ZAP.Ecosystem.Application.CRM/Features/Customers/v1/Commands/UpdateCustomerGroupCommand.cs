using MediatR;
using System;

namespace ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Commands
{
    public class UpdateCustomerGroupCommand : IRequest<object>
    {
        public string Id { get; set; } = string.Empty; // Injected from route
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal DiscountPercentage { get; set; }
    }
}

