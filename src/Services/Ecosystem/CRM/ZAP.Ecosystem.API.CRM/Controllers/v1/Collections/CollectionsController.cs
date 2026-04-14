using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.CRM.Features.Collections.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.Collections.v1.Queries;

namespace ZAP.Ecosystem.API.CRM.Controllers.v1.Collections;

[ApiController]
[Route("api/collections")]
public class CollectionsController : ControllerBase
{
    private readonly IMediator _mediator;
    public CollectionsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("list")]
    public async Task<IActionResult> List([FromBody] CollectionListRequestDto request)
    {
        var result = await _mediator.Send(new GetCollectionListQuery { Request = request });
        return Ok(result);
    }
}

