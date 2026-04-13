using ZAP.Ecosystem.Shared.Data;
using ZAP.Ecosystem.Application.CRM.Features.Brands.v1.Commands;
using ZAP.Ecosystem.Application.CRM.Features.Brands.v1.DTOs;
using ZAP.Ecosystem.Application.CRM.Features.Brands.v1.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.API.CRM.Features.Brands.v1.Controllers
{
    [ApiController]
    [Route("api/brands")]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("list")]
        [Consumes("application/json")]
        public async Task<IActionResult> List([FromBody] BrandListRequestDto requestBody)
        {
            var result = await _mediator.Send(new GetBrandListQuery { Request = requestBody });
            return Ok(new
            {
                success = true,
                code = 200,
                message = "OK",
                data = new
                {
                    total_page = result.PageSize > 0 ? (int)System.Math.Ceiling(result.TotalCount / (double)result.PageSize) : 1,
                    total_record = result.TotalCount,
                    page_index = result.CurrentPage,
                    page_size = result.PageSize,
                    items = result.Items
                }
            });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetBrandByIdQuery { Id = id });
            if (result == null)
                return NotFound(new { success = false, code = 404, message = "Brand not found", data = (object?)null });

            return Ok(new { success = true, code = 200, message = "OK", data = result });
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] CreateBrandCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { success = true, code = 200, message = "Created successfully", data = new { id } });
        }

        [HttpPut("{id:guid}")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBrandCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            if (!result)
                return NotFound(new { success = false, code = 404, message = "Brand not found", data = (object?)null });

            return Ok(new { success = true, code = 200, message = "Updated successfully", data = new { id } });
        }
    }
}


