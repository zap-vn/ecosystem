using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZAP.Ecosystem.Application.CRM.Features.PaymentTypes.v1.Commands;
using ZAP.Ecosystem.Application.CRM.Features.PaymentTypes.v1.Queries;
using ZAP.Ecosystem.Application.CRM.Features.PaymentTypes.v1.DTOs;
using ZAP.Ecosystem.Shared.Data;
using ZAP.Ecosystem.Shared.Data;

namespace ZAP.Ecosystem.API.CRM.Features.Payments.v1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new { Status = "CRM PaymentTypes API is running", Time = System.DateTime.UtcNow });
        }

        [HttpPost("list")]
        [Consumes("application/json")]
        public async Task<IActionResult> List([FromBody] FilterDTOs filter)
        {
            var result = await _mediator.Send(new GetPaymentTypeListQuery { Filter = filter });
            return Ok(new
            {
                success = true,
                code = 200,
                message = "OK",
                data = new
                {
                    total_page = result.PageSize > 0 ? (int)System.Math.Ceiling((double)result.TotalCount / result.PageSize) : 1,
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
            var result = await _mediator.Send(new GetPaymentTypeByIdQuery(id));
            if (result == null)
                return NotFound(new { success = false, code = 404, message = "PaymentType not found", data = (object?)null });

            return Ok(new { success = true, code = 200, message = "OK", data = result });
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] CreatePaymentTypeCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { success = true, code = 200, message = "Created successfully", data = new { id } });
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdatePaymentTypeCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            if (!result)
                return NotFound(new { success = false, code = 404, message = "PaymentType not found", data = (object?)null });

            return Ok(new { success = true, code = 200, message = "Updated successfully", data = new { id } });
        }
    }
}


