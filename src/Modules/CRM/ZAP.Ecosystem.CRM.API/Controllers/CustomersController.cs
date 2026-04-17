using Microsoft.AspNetCore.Mvc;
using MediatR;
using ZAP.Ecosystem.CRM.Application.Features.Customers.v1.Queries;
using ZAP.Ecosystem.CRM.Application.Features.Customers.v1.DTOs;
using ZAP.Ecosystem.Shared.Responses;

namespace ZAP.Ecosystem.CRM.API.Controllers;

[ApiController]
[Route("api/v1/crm/customers")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomersController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] CustomerListRequestDto request)
    {
        var query = new GetCustomerListQuery(request);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetCustomerByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}




