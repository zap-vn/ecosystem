using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.CRM.Features.DiningOptions.v1.Commands;
using ZAP.Ecosystem.Application.CRM.Features.DiningOptions.v1.Queries;

namespace ZAP.Ecosystem.API.CRM.Features.DiningOptions.v1.Controllers;

[ApiController]
[Route("api/diningoptions")]
public class DiningOptionsController : ControllerBase
{
    private readonly IMediator _mediator;
    public DiningOptionsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetDiningOptionListQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetDiningOptionByIdQuery { Id = id });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDiningOptionCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateDiningOptionCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
