using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.CRM.Features.ModifierGroup.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.ModifierGroup.v1.Queries;

namespace ZAP.Ecosystem.API.CRM.Controllers.v1.ModifierGroup;

[ApiController]
[Route("api/modifiergroups")]
public class ModifierGroupsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ModifierGroupsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("list")]
    public async Task<IActionResult> List([FromBody] ModifierGroupListRequestDto request)
    {
        var result = await _mediator.Send(new GetModifierGroupsQuery { Request = request });
        return Ok(result);
    }
}

