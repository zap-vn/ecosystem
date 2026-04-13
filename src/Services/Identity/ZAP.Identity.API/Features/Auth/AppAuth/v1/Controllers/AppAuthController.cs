// removed Asp.Versioning to prevent ambiguity
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZAP.Ecosystem.Shared.Responses;
using ZAP.Identity.API.Features.Shared.Controllers;
using ZAP.Identity.Application.Features.Auth.AppAuth.V1.Queries;

namespace ZAP.Identity.API.Features.Auth.AppAuth.V1.Controllers;

[Asp.Versioning.ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth/customer")]
public class AppAuthController : BaseApiController
{
    private readonly IMediator _mediator;

    public AppAuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCustomerQuery command)
    {
        try
        {
            var data = await _mediator.Send(command);
            return Ok(ApiResponse<LoginResponse>.Ok(data, "Login successful"));
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(ApiResponse<object>.Failure(401, "Invalid credentials", "UNAUTHORIZED"));
        }
    }
}
