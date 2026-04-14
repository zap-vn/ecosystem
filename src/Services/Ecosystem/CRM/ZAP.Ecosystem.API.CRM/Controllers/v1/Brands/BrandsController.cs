using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.CRM.Features.Brands.v1.Commands;
using ZAP.Ecosystem.Application.CRM.Features.Brands.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.Brands.v1.Queries;

namespace ZAP.Ecosystem.API.CRM.Controllers.v1.Brands;

[ApiController]
[Route("api/brands")]
public class BrandsController : ControllerBase
{
    private readonly IMediator _mediator;
    public BrandsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("list")]
    public async Task<IActionResult> List([FromBody] BrandListRequestDto request)
    {
        var result = await _mediator.Send(new GetBrandListQuery { Request = request });
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetBrandByIdQuery { Id = id });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBrandCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBrandCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}

