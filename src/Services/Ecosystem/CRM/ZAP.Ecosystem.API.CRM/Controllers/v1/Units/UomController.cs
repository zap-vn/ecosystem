using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.CRM.Features.Units.v1.Commands;
using ZAP.Ecosystem.Application.CRM.Features.Units.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.Units.v1.Queries;

namespace ZAP.Ecosystem.API.CRM.Controllers.v1.Units;

[ApiController]
[Route("api/uom")]
public class UomController : ControllerBase
{
    private readonly IMediator _mediator;
    public UomController(IMediator mediator) => _mediator = mediator;

    [HttpPost("list")]
    public async Task<IActionResult> List([FromBody] UnitListRequestDto request)
    {
        var result = await _mediator.Send(new GetUnitsQuery { Request = request });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUnitCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUnitCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteUnitCommand { Id = id });
        return Ok(result);
    }
}

