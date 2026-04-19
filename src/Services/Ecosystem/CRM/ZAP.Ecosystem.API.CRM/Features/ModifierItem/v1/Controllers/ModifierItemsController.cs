using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.CRM.Features.ModifierItem.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.ModifierItem.v1.Queries;

namespace ZAP.Ecosystem.API.CRM.Features.ModifierItem.v1.Controllers;

[ApiController]
[Route("api/v1/catalog/modifieritems")]
public class ModifierItemsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ModifierItemsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("list")]
    public async Task<IActionResult> List([FromBody] ModifierItemListRequestDto request)
    {
        var result = await _mediator.Send(new GetModifierItemsQuery { Request = request });
        return Ok(result);
    }
}
