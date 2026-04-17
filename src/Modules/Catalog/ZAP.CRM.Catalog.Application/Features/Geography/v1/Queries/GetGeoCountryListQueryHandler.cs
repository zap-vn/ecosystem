using ZAP.CRM.Catalog.Domain.Interfaces.Brands;
using ZAP.CRM.Catalog.Domain.Interfaces.Products;
using ZAP.CRM.Catalog.Domain.Interfaces.Menus;
using ZAP.CRM.Catalog.Domain.Interfaces.Categories;
using ZAP.CRM.Catalog.Domain.Interfaces.Locations;
using ZAP.CRM.Catalog.Domain.Interfaces.Modifiers;
using ZAP.CRM.Catalog.Domain.Interfaces.Geography;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using ZAP.Ecosystem.Shared.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZAP.Ecosystem.Shared.Responses;
using ZAP.CRM.Catalog.Application.Features.Geography.v1.DTOs;
using ZAP.CRM.Catalog.Domain;
using ZAP.Ecosystem.Shared.Data;

using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Menus;
using ZAP.CRM.Catalog.Domain.Entities.Categories;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using ZAP.CRM.Catalog.Domain.Entities.Modifiers;
using ZAP.CRM.Catalog.Domain.Entities.Geography;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using ZAP.CRM.Catalog.Domain.Interfaces;
namespace ZAP.CRM.Catalog.Application.Features.Geography.v1.Queries;
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
                // numeric_code can be stored as string or int in different domain assemblies; parse safely
                numeric_code  = int.TryParse(x.numeric_code?.ToString(), out var __nc) ? __nc : (int?)null,
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



