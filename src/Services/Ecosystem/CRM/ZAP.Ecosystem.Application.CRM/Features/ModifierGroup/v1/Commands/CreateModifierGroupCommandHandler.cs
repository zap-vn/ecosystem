using CRM.ModifierGroup.Domain.Entities;
using CRM.ModifierGroup.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.ModifierGroup.v1.Commands
{
    public class CreateModifierGroupCommandHandler : IRequestHandler<CreateModifierGroupCommand, Guid>
    {
        private readonly IModifierGroupRepository _repository;

        public CreateModifierGroupCommandHandler(IModifierGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateModifierGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.ModifierGroup
            {
                tenant_id = request.TenantId,
                legacy_id = request.LegacyId,
                name = request.Name,
                min_selection = request.MinSelection,
                max_selection = request.MaxSelection,
                is_required = request.IsRequired,
                sort_order = request.SortOrder
            };

            await _repository.CreateAsync(entity);
            return entity.id;
        }
    }
}

