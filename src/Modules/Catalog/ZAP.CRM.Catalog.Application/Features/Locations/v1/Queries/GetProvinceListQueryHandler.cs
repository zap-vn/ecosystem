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

public class GetProvinceListQueryHandler : IRequestHandler<GetProvinceListQuery, object>
{
    private readonly ILocationRepository _repository;

    public GetProvinceListQueryHandler(ILocationRepository repository)
    {
        _repository = repository;
    }

    public async Task<object> Handle(GetProvinceListQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetProvincesAsync(request.LocaleId);
        var dtos = items.Select(p => new ProvinceDto
        {
            province_code = p.code,
            city_name = p.translations?.FirstOrDefault(t => t.locale_id == request.LocaleId)?.name ?? string.Empty
        }).ToList();

        return CrmResponse.Ok(dtos);
    }
}



