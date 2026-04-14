using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Commands;
using ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Queries;

namespace ZAP.Ecosystem.API.CRM.Features.Customers.v1.Controllers;

[ApiController]
[Route("api/customergroups")]
public class CustomerGroupsController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomerGroupsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("list")]
    public async Task<IActionResult> List([FromBody] GetCustomerGroupListQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetCustomerGroupByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerGroupCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateCustomerGroupCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
