using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Commands;
using ZAP.Ecosystem.Application.CRM.Features.Payments.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Queries;

namespace ZAP.Ecosystem.API.CRM.Features.Payments.v1.Controllers;

[ApiController]
[Route("api/paymenttypes")]
public class PaymentTypesController : ControllerBase
{
    private readonly IMediator _mediator;
    public PaymentTypesController(IMediator mediator) => _mediator = mediator;

    [HttpPost("list")]
    public async Task<IActionResult> List([FromBody] PaymentTypeListRequestDto request)
    {
        var result = await _mediator.Send(new GetPaymentTypeListQuery { Request = request });
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetPaymentTypeByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePaymentTypeCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdatePaymentTypeCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
