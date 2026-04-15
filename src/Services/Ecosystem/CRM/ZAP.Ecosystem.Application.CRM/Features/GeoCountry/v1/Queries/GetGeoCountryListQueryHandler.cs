using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.CRM.Common;
using ZAP.Ecosystem.Application.CRM.Features.GeoCountry.v1.DTOs;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Shared.Data;

namespace ZAP.Ecosystem.Application.CRM.Features.GeoCountry.v1.Queries
{
    public class GetGeoCountryListQueryHandler : IRequestHandler<GetGeoCountryListQuery, object>
    {
        private readonly IGeoCountryRepository _repository;

        public GetGeoCountryListQueryHandler(IGeoCountryRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> Handle(GetGeoCountryListQuery request, CancellationToken cancellationToken)
        {
            var req = request.Request;

            var (items, total) = await _repository.GetPagedAsync(
                req.PageIndex,
                req.PageSize,
                req.Filters?.IsActive,
                req.Filters?.Search,
                req.Sort?.Field ?? "id",
                req.Sort?.Descending ?? false);

            var dtos = items.Select(x => new GeoCountryDto
            {
                id            = x.id,
                name          = x.Translations?.FirstOrDefault()?.name,
                serial_id     = x.serial_id,
                serial_number = x.serial_number,
                iso_alpha2    = x.iso_alpha2,
                iso_alpha3    = x.iso_alpha3,
                numeric_code  = x.numeric_code,
                is_active     = x.is_active,
                latitude      = x.latitude,
                longitude     = x.longitude,
                geometry_data = x.geometry_data,
                flag_emoji    = x.flag_emoji,
                flag_url      = x.flag_url,
                created_at    = x.created_at,
                updated_at    = x.updated_at,
            }).ToList();

            return CrmResponse.Paged(new PagedResult<GeoCountryDto>(dtos, total, req.PageIndex, req.PageSize));
        }
    }
}
