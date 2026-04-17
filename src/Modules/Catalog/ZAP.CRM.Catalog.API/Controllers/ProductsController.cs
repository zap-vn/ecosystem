using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.CRM.Catalog.Application.Features.Products.v1.Commands;
using ZAP.CRM.Catalog.Application.Features.Products.v1.DTOs;
using ZAP.CRM.Catalog.Application.Features.Products.v1.Queries;

namespace ZAP.CRM.Catalog.API.Controllers;

[ApiController]
[Route("api/v1/catalog/products")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("list")]
    public async Task<IActionResult> List([FromBody] ProductListRequestDto request)
    {
        var result = await _mediator.Send(new GetProductListQuery { Request = request });
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateProductCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}




