using Microsoft.AspNetCore.Mvc;

namespace ZAP.Ecosystem.Inventory.API.Controllers;

[ApiController]
[Route("api/v1/inventory/ping")]
public class InventoryPingController : ControllerBase
{
    [HttpGet]
    public IActionResult Ping() => Ok("Inventory Module API is active");
}









