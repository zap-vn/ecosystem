using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Sales.API.Controllers
{
    [ApiController]
    [Route("api/v1/sales/dining_options")]
    public class DiningOptionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DiningOptionsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(new {
                success = true,
                data = new[] {
                    new { id = System.Guid.NewGuid(), title = "Sample DiningOptions 1", status = "ACTIVE", created_at = System.DateTime.UtcNow },
                    new { id = System.Guid.NewGuid(), title = "Sample DiningOptions 2", status = "COMPLETED", created_at = System.DateTime.UtcNow.AddHours(-2) }
                },
                meta = new { page = 1, size = 10, total = 2 }
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            return Ok(new {
                success = true,
                message = "Operation on DiningOptions completed successfully",
                data = new {
                    id = System.Guid.NewGuid(),
                    processed_at = System.DateTime.UtcNow
                }
            });
        }
    }
}





