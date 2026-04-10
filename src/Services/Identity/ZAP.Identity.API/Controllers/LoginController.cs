using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using ZAP.Identity.Application.Features.Auth.AppAuth.v1.Commands.LoginUser;
using ZAP.Identity.Application.Features.Auth.AppAuth.v1.Commands.CheckAccountAvailability;
using ZAP.Identity.Application.Features.Auth.AppAuth.v1.Commands.SendOtp;

namespace ZAP.Identity.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [AllowAnonymous]
    public class LoginController : ControllerBase
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
            // command.IsLogin = true; 
            var result = await _mediator.Send(command);
            
            // Giả định kiểu dữ liệu trả về theo wrapper tiêu chuẩn của ZAP
            // return result.Success ? Ok(...) : BadRequest(...);
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
        [HttpPost("login")]
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
