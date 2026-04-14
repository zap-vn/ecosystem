using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Locations.v1.DTOs;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Queries;

public class GetLocationListQueryHandler : IRequestHandler<GetLocationListQuery, object>
{
    private readonly ILocationRepository _repository;

    public GetLocationListQueryHandler(ILocationRepository repository)
    {
        _repository = repository;
    }

    public async Task<object> Handle(GetLocationListQuery request, CancellationToken cancellationToken)
    {
        var req = request.Request;

        int localeId = req.locale_id;
        if (!string.IsNullOrEmpty(request.AcceptLanguage) && int.TryParse(request.AcceptLanguage, out var parsedLocale))
            localeId = parsedLocale;

        var filter = new LocationListFilter
        {
            PageIndex       = req.page_index,
            PageSize        = req.page_size,
            Search          = req.search,
            StatusId        = req.filters?.status_id,
            ProvinceId      = req.filters?.province_id,
            SortField       = req.sort?.field,
            SortDescending  = req.sort?.descending ?? false,
            LocationTypeIds = req.filters?.location_type_id,
        };

        var items = await _repository.GetPagedAsync(filter);
        var total = await _repository.GetTotalCountAsync(filter);

        var dtos = items.Select(x => new LocationDto
        {
            id                 = x.id,
            tenant_id          = x.tenant_id,
            serial_id          = x.serial_id,
            serial_number      = x.serial_number,
            name               = x.name,
            location_code      = x.location_code,
            status_id          = x.status_id,
            status_code        = x.status?.code,
            status_name        = x.status?.translations?.FirstOrDefault(t => t.locale_id == localeId)?.name
                              ?? x.status?.translations?.FirstOrDefault(t => t.locale_id == 2)?.name,
            is_active          = x.is_active,
            created_at         = x.created_at,
            updated_at         = x.updated_at,
            slug               = x.slug,
            business_name      = x.business_name,
            description        = x.description,
            location_type_id   = x.location_type_id,
            location_type_text = x.location_type?.translations?.FirstOrDefault(t => t.locale_id == localeId)?.name
                              ?? x.location_type?.translations?.FirstOrDefault(t => t.locale_id == 2)?.name,
            address_line_1     = x.address_line_1,
            address_line_2     = x.address_line_2,
            city               = x.city,
            state              = x.state,
            country_id         = x.country_id,
            province_id        = x.province_id,
            district_id        = x.district_id,
            ward_id            = x.ward_id,
            zipcode            = x.zipcode,
            phone_number       = x.phone_number,
            email              = x.email,
            website            = x.website,
            twitter            = x.twitter,
            instagram          = x.instagram,
            facebook           = x.facebook,
            logo_url           = x.logo_url,
            cover_image_url    = x.cover_image_url,
            brand_color        = x.brand_color,
            timezone           = x.timezone,
            latitude           = x.latitude,
            longitude          = x.longitude,
            operating_hours    = x.operating_hours,
            transfer_account   = x.transfer_account,
            transfer_tag       = x.transfer_tag,
            parent_location_id = x.parent_location_id
        }).ToList();

        return CrmResponse.Paged(new PagedResult<LocationDto>(dtos, total, req.page_index, req.page_size));
    }
}
