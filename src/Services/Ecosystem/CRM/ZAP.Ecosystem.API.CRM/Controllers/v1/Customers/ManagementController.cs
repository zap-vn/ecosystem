using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Queries;

namespace ZAP.Ecosystem.API.CRM.Controllers.v1.Customers;

[ApiController]
[Route("api/v1/customers/management")]
public class ManagementController : ControllerBase
{
    private readonly IMediator _mediator;
    public ManagementController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetCustomers([FromQuery] GetCustomerManagementListQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("memberships")]
    public async Task<IActionResult> GetMemberships([FromQuery] GetMembershipListQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}

