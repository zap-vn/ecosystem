using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.CRM.Features.Promotions.v1.Queries;

namespace ZAP.Ecosystem.API.CRM.Controllers.v1.Promotions;

[ApiController]
[Route("api/v1/promotions/management")]
public class ManagementController : ControllerBase
{
    private readonly IMediator _mediator;
    public ManagementController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetPromotions([FromQuery] GetPromotionListQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}

