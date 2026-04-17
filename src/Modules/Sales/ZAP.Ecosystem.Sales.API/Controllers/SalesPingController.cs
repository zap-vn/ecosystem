using Microsoft.AspNetCore.Mvc;

namespace ZAP.Ecosystem.Sales.API.Controllers;

[ApiController]
[Route("api/v1/sales/ping")]
public class SalesPingController : ControllerBase
{
    [HttpGet]
    public IActionResult Ping() => Ok("Sales Module API is active");
}









