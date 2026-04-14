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
    public class MenuRepository : BaseRepository<MenuHeader>, IMenuRepository
    {
        public MenuRepository(EcosystemDbContext context) : base(context)
        {
        }

        public override async Task<MenuHeader?> GetByIdAsync(object id, System.Threading.CancellationToken cancellationToken = default)
        {
            if (id is Guid guid)
                return await _dbSet.Include(m => m.status).FirstOrDefaultAsync(m => m.id == guid, cancellationToken);
            return await base.GetByIdAsync(id, cancellationToken);
        }

        public async Task<(IEnumerable<MenuHeader> Items, int Total)> GetPagedAsync(
            int page, int pageSize,
            Guid? tenantId = null, string? search = null,
            bool? isActive = null, string? menuType = null,
            int localeId = 2, string sortField = "name",
            bool sortDescending = false)
        {
            var query = _dbContext.Set<MenuHeader>()
                .Include(m => m.status)
                .ThenInclude(s => s.translations)
                .AsNoTracking();

            if (tenantId.HasValue) query = query.Where(m => m.tenant_id == tenantId.Value);
            if (isActive.HasValue) query = query.Where(m => m.is_active == isActive.Value);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(m => m.name.Contains(search));

            var totalCount = await query.CountAsync();

            query = sortField.ToLower() switch
            {
                "name" => sortDescending ? query.OrderByDescending(m => m.name) : query.OrderBy(m => m.name),
                "created_at" => sortDescending ? query.OrderByDescending(m => m.created_at) : query.OrderBy(m => m.created_at),
                _ => sortDescending ? query.OrderByDescending(m => m.name) : query.OrderBy(m => m.name)
            };

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task CreateAsync(MenuHeader menu)
        {
            await _dbSet.AddAsync(menu);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(MenuHeader menu)
        {
            _dbContext.Entry(menu).State = EntityState.Modified;
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

        Task<MenuHeader?> IMenuRepository.GetByIdAsync(Guid id) => GetByIdAsync(id);
    }
}
