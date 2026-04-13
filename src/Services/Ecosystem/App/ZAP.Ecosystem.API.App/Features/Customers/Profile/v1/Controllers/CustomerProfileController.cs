using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZAP.Ecosystem.Application.App.Features.Customers.Profile.v1.Queries;

namespace ZAP.Ecosystem.API.App.Features.Customers.Profile.v1.Controllers;

[Asp.Versioning.ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/customer")]
[ApiController]
public class CustomerProfileController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        try
        {
            var customerIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(customerIdString) || !Guid.TryParse(customerIdString, out var customerId))
            {
                return Unauthorized(new { Error = "Invalid token payload." });
            }

            var query = new GetCustomerProfileQuery { CustomerId = customerId };
            var profile = await _mediator.Send(query);

            return Ok(ZAP.Ecosystem.Shared.Responses.ApiResponse<CustomerProfileDto>.Ok(profile));
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
