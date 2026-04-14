using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;
using ZAP.Ecosystem.Shared.Data;

namespace ZAP.Ecosystem.Infrastructure.Repositories.CRM
{
    public class PromotionRepository : BaseRepository<PromotionEntity>, IPromotionRepository
    {
        public PromotionRepository(EcosystemDbContext context) : base(context)
        {
        }

        public async Task<PagedResult<PromotionEntity>> GetPagedAsync(
            int pageIndex, int pageSize,
            Guid? tenantId = null, string? search = null,
            int? statusId = null, int? discountTypeId = null,
            string sortField = "name", bool sortDescending = false)
        {
            var query = _dbSet.AsNoTracking();

            if (tenantId.HasValue) query = query.Where(p => p.tenant_id == tenantId.Value);
            if (statusId.HasValue) query = query.Where(p => p.status_id == statusId.Value);
            if (discountTypeId.HasValue) query = query.Where(p => p.discount_type_id == discountTypeId.Value);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(p => p.name.Contains(search));

            var totalCount = await query.CountAsync();

            query = sortField.ToLower() switch
            {
                "name" => sortDescending ? query.OrderByDescending(p => p.name) : query.OrderBy(p => p.name),
                "created_at" => sortDescending ? query.OrderByDescending(p => p.created_at) : query.OrderBy(p => p.created_at),
                _ => sortDescending ? query.OrderByDescending(p => p.name) : query.OrderBy(p => p.name)
            };

            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<PromotionEntity>(items, totalCount, pageIndex, pageSize);
        }

        // Standard IRepository implementation (if needed, but BaseRepository handles it)
        public async Task<PromotionEntity?> GetByIdAsync(Guid id) => await GetByIdAsync((object)id);
        public async Task<IEnumerable<PromotionEntity>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task AddAsync(PromotionEntity entity) => await AddAsync(entity, default);
        public async Task UpdateAsync(PromotionEntity entity) => await UpdateAsync(entity, default);
        public async Task DeleteAsync(Guid id) 
        {
            var entity = await GetByIdAsync(id);
            if (entity != null) await DeleteAsync(entity, default);
        }
    }
}
