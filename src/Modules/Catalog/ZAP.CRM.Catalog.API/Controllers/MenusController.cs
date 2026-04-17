using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ZAP.CRM.Catalog.Application.Features.Menus.v1.Commands;
using ZAP.CRM.Catalog.Application.Features.Menus.v1.DTOs;
using ZAP.CRM.Catalog.Application.Features.Menus.v1.Queries;

namespace ZAP.CRM.Catalog.API.Controllers;

[ApiController]
[Route("api/v1/catalog/menus")]
public class MenusController : ControllerBase
{
    private readonly IMediator _mediator;
    public MenusController(IMediator mediator) => _mediator = mediator;

    [HttpPost("list")]
    public async Task<IActionResult> List([FromBody] MenuListRequestDto? request)
    {
        var result = await _mediator.Send(new GetMenuListQuery { Request = request ?? new() });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMenuCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMenuCommand command)
    {
        command.id = id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteMenuCommand { id = id });
        return Ok(result);
    }
}




