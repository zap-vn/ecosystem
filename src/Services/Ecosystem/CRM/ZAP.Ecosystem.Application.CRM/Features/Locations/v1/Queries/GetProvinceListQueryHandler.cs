using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CRM.Location.Application.Features.Locations.DTOs;
using CRM.Location.Domain.Interfaces;
using CRM.BuildingBlocks.Interfaces;

namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Queries
{
    public class GetProvinceListQueryHandler : IRequestHandler<GetProvinceListQuery, List<ProvinceDto>>
    {
        private readonly ILocationRepository _repository;
        private readonly ICurrentUserService _currentUserService;

        public GetProvinceListQueryHandler(ILocationRepository repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<List<ProvinceDto>> Handle(GetProvinceListQuery request, CancellationToken cancellationToken)
        {
            var localeId = _currentUserService.LocaleId;
            var items = await _repository.GetProvincesAsync(localeId);

            return items.Select(p =>
            {
                var translation = p.translations?.FirstOrDefault(t => t.locale_id == localeId);
                return new ProvinceDto
                {
                    province_code = p.code,
                    city_name = translation?.name ?? string.Empty
                };
            }).ToList();
        }
    }
}


