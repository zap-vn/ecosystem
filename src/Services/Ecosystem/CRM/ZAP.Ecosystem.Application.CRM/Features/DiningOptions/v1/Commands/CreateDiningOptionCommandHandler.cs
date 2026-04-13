using CRM.DiningOption.Domain.Entities;
using CRM.DiningOption.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.DiningOptions.v1.Commands
{
    public class CreateDiningOptionCommandHandler : IRequestHandler<CreateDiningOptionCommand, int>
    {
        private readonly IDiningOptionRepository _repository;

        public CreateDiningOptionCommandHandler(IDiningOptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateDiningOptionCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.DiningOption {
                code = request.Code,
                is_active = request.IsActive,
                sort_order = request.SortOrder
            };

            await _repository.CreateAsync(entity);
            return entity.id;
        }
    }
}



