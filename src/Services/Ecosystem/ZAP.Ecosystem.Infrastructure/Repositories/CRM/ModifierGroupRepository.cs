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
    public class ModifierGroupRepository : BaseRepository<ModifierGroup>, IModifierGroupRepository
    {
        public ModifierGroupRepository(EcosystemDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ModifierGroup>> GetAllAsync(Guid? tenantId = null)
        {
            var query = _dbSet.AsNoTracking();
            if (tenantId.HasValue) query = query.Where(g => g.tenant_id == tenantId.Value);
            return await query.ToListAsync();
        }

        public override async Task<ModifierGroup?> GetByIdAsync(object id, System.Threading.CancellationToken cancellationToken = default)
        {
            if (id is Guid guid)
                return await _dbSet.Include(g => g.status).FirstOrDefaultAsync(g => g.id == guid, cancellationToken);
            return await base.GetByIdAsync(id, cancellationToken);
        }

        public async Task<(IEnumerable<ModifierGroup> Items, int Total)> GetPagedAsync(
            int page, int pageSize,
            Guid? tenantId = null, string? search = null,
            int? statusId = null, string? displayType = null,
            string sortField = "name", bool sortDescending = false)
        {
            var query = _dbContext.Set<ModifierGroup>()
                .Include(g => g.status)
                .ThenInclude(s => s.translations)
                .AsNoTracking();

            if (tenantId.HasValue) query = query.Where(g => g.tenant_id == tenantId.Value);
            if (statusId.HasValue) query = query.Where(g => g.status_id == statusId.Value);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(g => g.name.Contains(search));

            var totalCount = await query.CountAsync();

            query = sortField.ToLower() switch
            {
                "name" => sortDescending ? query.OrderByDescending(g => g.name) : query.OrderBy(g => g.name),
                "sort_order" => sortDescending ? query.OrderByDescending(g => g.sort_order) : query.OrderBy(g => g.sort_order),
                _ => sortDescending ? query.OrderByDescending(g => g.name) : query.OrderBy(g => g.name)
            };

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task CreateAsync(ModifierGroup modifierGroup)
        {
            await _dbSet.AddAsync(modifierGroup);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ModifierGroup modifierGroup)
        {
            _dbContext.Entry(modifierGroup).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        Task<ModifierGroup?> IModifierGroupRepository.GetByIdAsync(Guid id) => GetByIdAsync(id);
    }
}
