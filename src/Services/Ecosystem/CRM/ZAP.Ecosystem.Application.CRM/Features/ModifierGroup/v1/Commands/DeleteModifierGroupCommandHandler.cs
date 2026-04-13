using CRM.ModifierGroup.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.ModifierGroup.v1.Commands
{
    public class DeleteModifierGroupCommandHandler : IRequestHandler<DeleteModifierGroupCommand, bool>
    {
        private readonly IModifierGroupRepository _repository;

        public DeleteModifierGroupCommandHandler(IModifierGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteModifierGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) return false;

            await _repository.DeleteAsync(request.Id);
            return true;
        }
    }
}

