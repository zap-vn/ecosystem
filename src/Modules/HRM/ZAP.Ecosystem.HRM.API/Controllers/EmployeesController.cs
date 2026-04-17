using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.Ecosystem.HRM.Domain.Interfaces;
using System;

namespace ZAP.Ecosystem.HRM.API.Controllers
{
    [ApiController]
    [Route("api/v1/hrm/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        public EmployeesController(IEmployeeRepository repository) => _repository = repository;

        [HttpGet("list")]
        [HttpPost("list")]
        public async Task<IActionResult> List([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _repository.GetPagedAsync(page, pageSize);
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






