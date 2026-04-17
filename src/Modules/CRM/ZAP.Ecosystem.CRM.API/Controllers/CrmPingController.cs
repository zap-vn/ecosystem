using Microsoft.AspNetCore.Mvc;

namespace ZAP.Ecosystem.CRM.API.Controllers;

[ApiController]
[Route("api/v1/crm/ping")]
public class CrmPingController : ControllerBase
{
    [HttpGet]
    public IActionResult Ping() => Ok("CRM API is active");
}




