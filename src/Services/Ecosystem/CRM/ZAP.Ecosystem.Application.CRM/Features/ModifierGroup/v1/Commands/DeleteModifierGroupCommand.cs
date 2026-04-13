using System;
using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.ModifierGroup.v1.Commands
{
    public class DeleteModifierGroupCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}

