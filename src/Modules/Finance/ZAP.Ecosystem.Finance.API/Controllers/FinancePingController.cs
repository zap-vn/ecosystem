using Microsoft.AspNetCore.Mvc;

namespace ZAP.Ecosystem.Finance.API.Controllers;

[ApiController]
[Route("api/v1/finance/ping")]
public class FinancePingController : ControllerBase
{
    [HttpGet]
    public IActionResult Ping() => Ok("Finance Module API is active");
}









