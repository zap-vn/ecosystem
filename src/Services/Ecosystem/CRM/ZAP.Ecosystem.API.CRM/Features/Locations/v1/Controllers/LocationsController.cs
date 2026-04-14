using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Commands;
using ZAP.Ecosystem.Application.CRM.Features.Locations.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Queries;

namespace ZAP.Ecosystem.API.CRM.Features.Locations.v1.Controllers;

[ApiController]
[Route("api/locations")]
public class LocationsController : ControllerBase
{
    private readonly IMediator _mediator;
    public LocationsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("list")]
    public async Task<IActionResult> List(
        [FromBody] LocationListRequestDto request,
        [FromHeader(Name = "Accept-Language")] string? acceptLanguage)
    {
        var result = await _mediator.Send(new GetLocationListQuery { Request = request, AcceptLanguage = acceptLanguage });
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        [FromHeader(Name = "Accept-Language")] string? acceptLanguage)
    {
        var result = await _mediator.Send(new GetLocationByIdQuery { Id = id, AcceptLanguage = acceptLanguage });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLocationCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLocationCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("provinces")]
    public async Task<IActionResult> GetProvinces([FromQuery] int locale_id = 2)
    {
        var result = await _mediator.Send(new GetProvinceListQuery { LocaleId = locale_id });
        return Ok(result);
    }
}
