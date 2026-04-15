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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(EcosystemDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetAllAsync(Guid? tenantId = null)
        {
            var query = _dbSet.AsNoTracking();
            if (tenantId.HasValue) query = query.Where(c => c.tenant_id == tenantId.Value);
            return await query.ToListAsync();
        }

        public override async Task<Category?> GetByIdAsync(object id, System.Threading.CancellationToken cancellationToken = default)
        {
            if (id is Guid guid)
                return await _dbSet.Include(c => c.status).FirstOrDefaultAsync(c => c.id == guid, cancellationToken);
            return await base.GetByIdAsync(id, cancellationToken);
        }

        public async Task<(IEnumerable<Category> Items, int Total)> GetPagedAsync(
            int page, int pageSize,
            Guid? tenantId = null, string? search = null,
            int? statusId = null, Guid? parentId = null,
            string sortField = "name", bool sortDescending = false)
        {
            var query = _dbContext.Set<Category>()
                .Include(c => c.status)
                .ThenInclude(s => s.translations)
                .AsNoTracking();

            if (tenantId.HasValue) query = query.Where(c => c.tenant_id == tenantId.Value);
            if (statusId.HasValue) query = query.Where(c => c.status_id == statusId.Value);
            if (parentId.HasValue) query = query.Where(c => c.parent_id == parentId.Value);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(c => c.name.Contains(search));

            var totalCount = await query.CountAsync();

            query = sortField.ToLower() switch
            {
                "name" => sortDescending ? query.OrderByDescending(c => c.name) : query.OrderBy(c => c.name),
                "sort_order" => sortDescending ? query.OrderByDescending(c => c.sort_order) : query.OrderBy(c => c.sort_order),
                _ => sortDescending ? query.OrderByDescending(c => c.name) : query.OrderBy(c => c.name)
            };

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task CreateAsync(Category category)
        {
            await _dbSet.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _dbContext.Entry(category).State = EntityState.Modified;
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

        Task<Category?> ICategoryRepository.GetByIdAsync(Guid id) => GetByIdAsync(id);
    }
}
