using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.CRM.Features.GeoCountry.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.GeoCountry.v1.Queries;

namespace ZAP.Ecosystem.API.CRM.Features.GeoCountry.v1.Controllers;

[ApiController]
[Route("api/v1/catalog/geocountries")]
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
