using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.CRM.Catalog.Application.Features.Modifiers.v1.DTOs;
using ZAP.CRM.Catalog.Application.Features.Modifiers.v1.Queries;

namespace ZAP.CRM.Catalog.API.Controllers;

[ApiController]
[Route("api/v1/catalog/modifier_groups")]
public class ModifierGroupsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ModifierGroupsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("list")]
    public async Task<IActionResult> List([FromBody] ModifierItemListRequestDto? request)
    {
        var result = await _mediator.Send(new GetModifierItemsQuery { Request = request ?? new() });
        return Ok(result);
    }
}




