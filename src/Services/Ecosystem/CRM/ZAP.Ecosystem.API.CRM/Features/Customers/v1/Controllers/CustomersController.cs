using ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Commands;
using ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Queries;
using ZAP.Ecosystem.Application.CRM.Features.Customers.v1.DTOs;
using ZAP.Ecosystem.Shared.Data;
using ZAP.Ecosystem.Shared.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ZAP.Ecosystem.API.CRM.Features.Customers.v1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new { Status = "CRM Customer API is running", Time = System.DateTime.UtcNow });
        }



        [HttpPost("list")]
        public async Task<IActionResult> List([FromBody] CustomerListRequestDto requestBody)
        {
            var result = await _mediator.Send(new GetCustomerListQuery { Request = requestBody });
            return Ok(new
            {
                success = true,
                code = 200,
                message = "OK",
                data = new
                {
                    total_page = result.PageSize > 0 ? (result.TotalCount + result.PageSize - 1) / result.PageSize : 1,
                    total_record = result.TotalCount,
                    page_index = result.CurrentPage,
                    page_size = result.PageSize,
                    items = result.Items
                }
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateCustomerCommand command)
        {
            command.Id = id; 
            var result = await _mediator.Send(command);
            if (!result) return NotFound();
            return Ok(result);
        }
    }
}


