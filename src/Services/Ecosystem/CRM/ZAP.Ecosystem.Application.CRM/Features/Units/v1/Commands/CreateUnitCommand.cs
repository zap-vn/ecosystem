using System;
using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Units.v1.Commands
{
    public class CreateUnitCommand : IRequest<object>
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? UomType { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

