using MediatR;
using System;

namespace ZAP.Ecosystem.CRM.Application.Features.Customers.v1.Commands;
    public class CreateCustomerGroupCommand : IRequest<object>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal DiscountPercentage { get; set; }
    }




