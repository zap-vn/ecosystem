using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using ZAP.Identity.Application.Features.Auth.Login.v1.Commands.LoginUser;
using ZAP.Identity.Application.Features.Auth.Login.v1.Commands.CheckAccountAvailability;
using ZAP.Identity.Application.Features.Auth.Login.v1.Commands.SendOtp;
using ZAP.Identity.API.Features.Shared.Controllers;
using Asp.Versioning;

namespace ZAP.Identity.API.Features.Auth.Login.v1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth/login")]
    [AllowAnonymous]
    public class LoginController : BaseApiController
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 1. Check account (Login)
        /// </summary>
        [HttpPost("check-account")]
        public async Task<IActionResult> CheckAccount([FromBody] CheckAccountAvailabilityCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// 2. Send OTP (Login)
        /// </summary>
        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// 3. Verify OTP & Login
        /// </summary>
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtpAndLogin([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// 4. Login with password
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> LoginWithPassword([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new { Status = "Login API is running", Time = System.DateTime.UtcNow });
        }
    }
}
