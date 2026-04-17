using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.Ecosystem.Sales.Domain.Interfaces;
using System;

namespace ZAP.Ecosystem.Sales.API.Controllers
{
    [ApiController]
    [Route("api/v1/sales/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repository;
        public OrdersController(IOrderRepository repository) => _repository = repository;

        [HttpGet("list")]
        [HttpPost("list")] // Supporting both for Postman flexibility
        public async Task<IActionResult> List([FromQuery] string? keyword, [FromQuery] string? status, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var (items, total) = await _repository.GetOrderListAsync(keyword, status, pageIndex, pageSize);
            return Ok(new {
                success = true,
                data = items,
                meta = new { page = pageIndex, size = pageSize, total = total }
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






