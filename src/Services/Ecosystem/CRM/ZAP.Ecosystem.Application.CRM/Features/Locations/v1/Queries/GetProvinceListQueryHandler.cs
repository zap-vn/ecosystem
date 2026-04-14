using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Locations.v1.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Queries;

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
