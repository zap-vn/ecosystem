using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAP.Ecosystem.Shared.Entities;
using ZAP.Ecosystem.Shared.Responses;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.API.Controllers;

[ApiController]
[Route("api/v1/system/dictionaries")]
public class DictionariesController(EcosystemDbContext context) : ControllerBase
{
    [HttpGet("entities")]
    public async Task<IActionResult> ListEntities()
    {
        var entities = await context.EntityDictionaries
            .Include(e => e.Fields)
            .OrderBy(e => e.SchemaName).ThenBy(e => e.TableName)
            .ToListAsync();
            
        return Ok(CrmResponse.Ok(entities));
    }

    [HttpGet("entities/{schema}/{table}")]
    public async Task<IActionResult> GetEntity(string schema, string table)
    {
        var entity = await context.EntityDictionaries
            .Include(e => e.Fields.OrderBy(f => f.SortOrder))
            .FirstOrDefaultAsync(e => e.SchemaName == schema && e.TableName == table);
            
        if (entity == null) return NotFound(CrmResponse.NotFound("Entity dictionary"));
        
        return Ok(CrmResponse.Ok(entity));
    }

    [HttpPost("entities")]
    public async Task<IActionResult> CreateEntity([FromBody] EntityDictionary entity)
    {
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTime.UtcNow;
        
        context.EntityDictionaries.Add(entity);
        await context.SaveChangesAsync();
        
        return Ok(CrmResponse.Created(entity));
    }

    [HttpPost("fields")]
    public async Task<IActionResult> CreateField([FromBody] EntityFieldDictionary field)
    {
        field.Id = Guid.NewGuid();
        field.CreatedAt = DateTime.UtcNow;
        
        context.EntityFieldDictionaries.Add(field);
        await context.SaveChangesAsync();
        
        return Ok(CrmResponse.Created(field));
    }

    [HttpPut("fields/{id:guid}")]
    public async Task<IActionResult> UpdateField(Guid id, [FromBody] EntityFieldDictionary request)
    {
        var field = await context.EntityFieldDictionaries.FindAsync(id);
        if (field == null) return NotFound(CrmResponse.NotFound("Field dictionary"));
        
        field.DisplayName = request.DisplayName;
        field.IsVisibleList = request.IsVisibleList;
        field.IsVisibleDetail = request.IsVisibleDetail;
        field.IsRequired = request.IsRequired;
        field.SortOrder = request.SortOrder;
        // Keep existing properties intact
        field.UpdatedAt = DateTime.UtcNow;
        
        await context.SaveChangesAsync();
        return Ok(CrmResponse.Updated(field));
    }
}
