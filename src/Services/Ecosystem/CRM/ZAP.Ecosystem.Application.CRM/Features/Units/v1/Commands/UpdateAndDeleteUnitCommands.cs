using System;
using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Units.v1.Commands
{
    public class UpdateUnitCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? UomType { get; set; }
        public bool? IsActive { get; set; }
    }

    public class DeleteUnitCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}

