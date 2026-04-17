using ZAP.Ecosystem.CRM.Domain.Entities.Promotions;
using ZAP.Ecosystem.CRM.Domain.Interfaces.Promotions;
using ZAP.Ecosystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM.Promotions;

public class PromotionRepository : ZAP.Ecosystem.Shared.Data.BaseRepository<PromotionEntity>, IPromotionRepository
{
    public PromotionRepository(EcosystemDbContext context) : base(context) { }

    public async Task<ZAP.Ecosystem.Shared.Data.PagedResult<PromotionEntity>> GetPagedAsync(
        int pageIndex, int pageSize,
        Guid? tenantId = null, string? search = null,
        int? statusId = null, int? discountTypeId = null,
        string sortField = "name", bool sortDescending = false)
    {
        var query = _dbContext.Set<PromotionEntity>()
            .AsNoTracking()
            .AsQueryable();

        if (tenantId.HasValue)
            query = query.Where(p => p.tenant_id == tenantId);

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.name.Contains(search) || 
                                   (p.short_name != null && p.short_name.Contains(search)));
        }

        if (statusId.HasValue)
            query = query.Where(p => p.status_id == statusId);

        if (discountTypeId.HasValue)
            query = query.Where(p => p.discount_type_id == discountTypeId);

        // Sorting
        query = sortField.ToLower() switch
        {
            "name" => sortDescending ? query.OrderByDescending(p => p.name) : query.OrderBy(p => p.name),
            "created_at" => sortDescending ? query.OrderByDescending(p => p.created_at) : query.OrderBy(p => p.created_at),
            "status" => sortDescending ? query.OrderByDescending(p => p.status_id) : query.OrderBy(p => p.status_id),
            _ => sortDescending ? query.OrderByDescending(p => p.name) : query.OrderBy(p => p.name)
        };

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new ZAP.Ecosystem.Shared.Data.PagedResult<PromotionEntity>(items, totalCount, pageIndex, pageSize);
    }
}




