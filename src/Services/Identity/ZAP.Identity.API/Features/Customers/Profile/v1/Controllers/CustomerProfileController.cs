using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZAP.Identity.API.Features.Shared.Controllers;
using ZAP.Identity.Application.Features.Customers.Profile.V1.Queries;

namespace ZAP.Identity.API.Features.Customers.Profile.V1.Controllers;

[Asp.Versioning.ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/customer")]
public class CustomerProfileController : BaseApiController
{
    private readonly IMediator _mediator;

    public CustomerProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("profile")]
    // [Authorize] -> Bắt buộc có token (Đã tự động bảo vệ qua Global Filter)
    public async Task<IActionResult> GetProfile()
    {
        try
        {
            // Trích xuất CustomerId tự động từ JWT Token do Authenticated Middleware xử lý
            var customerIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(customerIdString) || !Guid.TryParse(customerIdString, out var customerId))
            {
                return Unauthorized(new { Error = "Invalid token payload." });
            }

            var query = new GetCustomerProfileQuery { CustomerId = customerId };
            var profile = await _mediator.Send(query);

            return Ok(new { Data = profile });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}
