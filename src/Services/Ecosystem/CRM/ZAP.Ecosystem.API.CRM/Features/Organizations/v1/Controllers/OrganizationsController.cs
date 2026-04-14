using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ZAP.Ecosystem.API.CRM.Features.Organizations.v1.Controllers;

[ApiController]
[Route("api/organizations")]
public class OrganizationsController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrganizationsController(IMediator mediator) => _mediator = mediator;

    [HttpGet("health")]
    public IActionResult Health() =>
        Ok(new { Status = "CRM Organization API is running", Time = System.DateTime.UtcNow });
}
