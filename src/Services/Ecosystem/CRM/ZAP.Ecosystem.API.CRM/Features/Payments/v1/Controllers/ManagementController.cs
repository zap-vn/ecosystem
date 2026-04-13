using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZAP.Ecosystem.Shared.Data;
using ZAP.Ecosystem.Application.CRM.Features.Payments.v1.Queries;
using ZAP.Ecosystem.Application.CRM.Features.Payments.v1.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ZAP.Ecosystem.API.CRM.Features.Payments.v1.Controllers
{
    [ApiController]
    [Route("api/v1/management")]
    public class ManagementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("payments")]
        public async Task<IActionResult> GetPayments([FromQuery] GetPaymentListQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(ApiResponse<IReadOnlyList<PaymentListDto>>.SuccessResult(
                result.Items, 
                new PaginationMetadata(result.CurrentPage, result.PageSize, result.TotalCount)));
        }
    }
}


