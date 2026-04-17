using ZAP.CRM.Catalog.Domain.Interfaces.Brands;
using ZAP.CRM.Catalog.Domain.Interfaces.Products;
using ZAP.CRM.Catalog.Domain.Interfaces.Menus;
using ZAP.CRM.Catalog.Domain.Interfaces.Categories;
using ZAP.CRM.Catalog.Domain.Interfaces.Locations;
using ZAP.CRM.Catalog.Domain.Interfaces.Modifiers;
using ZAP.CRM.Catalog.Domain.Interfaces.Geography;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using ZAP.Ecosystem.Shared.Entities;
using ZAP.Ecosystem.Shared.Responses;
using ZAP.Ecosystem.Shared.Data;
using ZAP.Ecosystem.Shared.Interfaces;
using ZAP.CRM.Catalog.Domain;
using MediatR;
using ZAP.CRM.Catalog.Application.Features.Locations.v1.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Menus;
using ZAP.CRM.Catalog.Domain.Entities.Categories;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using ZAP.CRM.Catalog.Domain.Entities.Modifiers;
using ZAP.CRM.Catalog.Domain.Entities.Geography;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using ZAP.CRM.Catalog.Domain.Interfaces;
namespace ZAP.CRM.Catalog.Application.Features.Locations.v1.Queries;

public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, object>
{
    private readonly ILocationRepository _repository;

    private readonly ZAP.Ecosystem.Shared.Interfaces.ICurrentUserService _currentUserService;
    public GetLocationByIdQueryHandler(ILocationRepository repository, ZAP.Ecosystem.Shared.Interfaces.ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<object> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
    {
        int localeId = _currentUserService.LocaleId > 0 ? _currentUserService.LocaleId : 2;

        var x = await _repository.GetByIdAsync(request.Id);
        if (x == null) return CrmResponse.NotFound("Location");

        return CrmResponse.Ok(new LocationDto
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
        });
    }
}



