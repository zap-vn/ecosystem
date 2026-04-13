using System;
using MediatR;

namespace ZAP.Ecosystem.Application.CRM.Features.Products.v1.Commands
{
    public class UpdateModifierGroupCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int MinSelection { get; set; }
        public int MaxSelection { get; set; }
        public bool IsRequired { get; set; }
        public int SortOrder { get; set; }
    }
}

