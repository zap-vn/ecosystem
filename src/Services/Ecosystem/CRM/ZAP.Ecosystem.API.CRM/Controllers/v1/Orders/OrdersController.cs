using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.CRM.Features.Orders.v1.Queries;

namespace ZAP.Ecosystem.API.CRM.Controllers.v1.Orders;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrdersController(IMediator mediator) => _mediator = mediator;

    [HttpGet("health")]
    public IActionResult Health() =>
        Ok(new { Status = "CRM Order API is running", Time = System.DateTime.UtcNow });

    [HttpPost("list")]
    public async Task<IActionResult> List([FromBody] GetTransactionManagementListQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}

