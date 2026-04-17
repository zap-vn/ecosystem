using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.Ecosystem.CRM.Domain.Interfaces.Promotions;
using System;

namespace ZAP.Ecosystem.CRM.API.Controllers
{
    [ApiController]
    [Route("api/v1/crm/promotions")]
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionRepository _repository;
        public PromotionsController(IPromotionRepository repository) => _repository = repository;

        [HttpGet("list")]
        [HttpPost("list")]
        public async Task<IActionResult> List([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var result = await _repository.GetPagedAsync(page, pageSize, null, search);
            return Ok(new {
                success = true,
                data = result.Items,
                meta = new { page = page, size = pageSize, total = result.TotalCount }
            });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(new { success = true, data = entity });
        }
    }
}
