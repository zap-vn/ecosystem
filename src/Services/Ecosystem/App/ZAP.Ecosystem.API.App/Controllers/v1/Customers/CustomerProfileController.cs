using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.App.Features.Customers.Profile.v1.Queries;

namespace ZAP.Ecosystem.API.App.Controllers.v1.Customers
{
    [ApiController]
    [Asp.Versioning.ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/customer")]
    public class CustomerProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile([FromQuery] Guid customerId)
        {
            var result = await _mediator.Send(new GetCustomerProfileQuery { CustomerId = customerId });
            return Ok(result);
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new { Status = "App API is running", Feature = "Customer Profile" });
        }
    }
}
