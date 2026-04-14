using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.CRM.Features.Orders.v1.Queries;

namespace ZAP.Ecosystem.API.CRM.Features.Orders.v1.Controllers;

[ApiController]
[Route("api/v1/management")]
public class ManagementController : ControllerBase
{
    private readonly IMediator _mediator;
    public ManagementController(IMediator mediator) => _mediator = mediator;

    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions([FromQuery] GetTransactionManagementListQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
