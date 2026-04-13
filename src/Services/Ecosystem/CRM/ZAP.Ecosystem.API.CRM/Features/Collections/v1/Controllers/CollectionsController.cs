using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZAP.Ecosystem.Application.CRM.Features.Collections.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.Collections.v1.Queries;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.API.CRM.Features.Collections.v1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CollectionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("list")]
        public async Task<IActionResult> List([FromBody] CollectionListRequestDto request)
        {
            var query = new GetCollectionListQuery
            {
                PageIndex = request.page_index,
                PageSize = request.page_size,
                Search = request.search
            };

            var result = await _mediator.Send(query);
            return Ok(new { success = true, data = result });
        }
    }
}



