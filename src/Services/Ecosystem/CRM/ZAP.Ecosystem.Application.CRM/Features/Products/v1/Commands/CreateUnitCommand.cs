using System;
using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.Commands
{
    public class CreateUnitCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? UomType { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

