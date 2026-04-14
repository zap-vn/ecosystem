using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Commands;
using ZAP.Ecosystem.Application.CRM.Features.Payments.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Queries;

namespace ZAP.Ecosystem.API.CRM.Controllers.v1.Payments;

[ApiController]
[Route("api/paymentterms")]
public class PaymentTermsController : ControllerBase
{
    private readonly IMediator _mediator;
    public PaymentTermsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("list")]
    public async Task<IActionResult> List([FromBody] GetPaymentTermsListQuery request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetPaymentTermsByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePaymentTermsCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdatePaymentTermsCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}

