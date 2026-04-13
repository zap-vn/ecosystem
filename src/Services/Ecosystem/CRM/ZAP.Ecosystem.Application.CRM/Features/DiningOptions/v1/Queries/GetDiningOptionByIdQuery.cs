using CRM.DiningOption.Application.Features.DiningOptions.DTOs;
using CRM.DiningOption.Domain.Interfaces;
using CRM.BuildingBlocks.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.DiningOptions.v1.Queries
{
    public class GetDiningOptionByIdQuery : IRequest<DiningOptionDto?>
    {
        public int Id { get; set; }
    }

    public class GetDiningOptionByIdQueryHandler : IRequestHandler<GetDiningOptionByIdQuery, DiningOptionDto?>
    {
        private readonly IDiningOptionRepository _repository;
        private readonly ILocalizationService _localizationService;

        public GetDiningOptionByIdQueryHandler(IDiningOptionRepository repository, ILocalizationService localizationService)
        {
            _repository = repository;
            _localizationService = localizationService;
        }

        public async Task<DiningOptionDto?> Handle(GetDiningOptionByIdQuery request, CancellationToken cancellationToken)
        {
            var localeId = _localizationService.GetCurrentLocaleId();
            var entity = await _repository.GetByIdAsync(request.Id, localeId);
            if (entity == null) return null;

            return new DiningOptionDto
            {
                id = entity.id,
                code = entity.code,
                display_name = entity.DisplayName ?? entity.code,
                used_in_locations = entity.UsedInLocations,
                sort_order = entity.sort_order,
                is_active = entity.is_active
            };
        }
    }
}


