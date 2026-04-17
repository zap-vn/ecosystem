using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.CRM.Catalog.Application.Features.Geography.v1.DTOs;
using ZAP.CRM.Catalog.Application.Features.Geography.v1.Queries;

namespace ZAP.CRM.Catalog.API.Controllers;

[ApiController]
[Route("api/v1/catalog/geo_countries")]
public class GeoCountriesController : ControllerBase
{
    private readonly IMediator _mediator;
    public GeoCountriesController(IMediator mediator) => _mediator = mediator;

    [HttpPost("list")]
    public async Task<IActionResult> List([FromBody] GeoCountryListRequestDto request)
    {
        var result = await _mediator.Send(new GetGeoCountryListQuery { Request = request });
        return Ok(result);
    }
}






