using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.HRM.API.Controllers
{
    [ApiController]
    [Route("api/v1/hrm/leaves")]
    public class LeavesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LeavesController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(new {
                success = true,
                data = new[] {
                    new { id = System.Guid.NewGuid(), title = "Sample Leaves 1", status = "ACTIVE", created_at = System.DateTime.UtcNow },
                    new { id = System.Guid.NewGuid(), title = "Sample Leaves 2", status = "COMPLETED", created_at = System.DateTime.UtcNow.AddHours(-2) }
                },
                meta = new { page = 1, size = 10, total = 2 }
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            return Ok(new {
                success = true,
                message = "Operation on Leaves completed successfully",
                data = new {
                    id = System.Guid.NewGuid(),
                    processed_at = System.DateTime.UtcNow
                }
            });
        }
    }
}





