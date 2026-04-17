using Microsoft.AspNetCore.Mvc;

namespace ZAP.Ecosystem.HRM.API.Controllers;

[ApiController]
[Route("api/v1/hrm/ping")]
public class HRMPingController : ControllerBase
{
    [HttpGet]
    public IActionResult Ping() => Ok("HRM Module API is active");
}









