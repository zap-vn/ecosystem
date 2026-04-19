using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class CategoryRepository : ZAP.Ecosystem.Shared.Data.BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(EcosystemDbContext context) : base(context) { }

        public async Task<IEnumerable<Category>> GetAllAsync(Guid? tenantId = null) => await _dbSet.Where(x => tenantId == null || x.tenant_id == tenantId).ToListAsync();
        public async Task<Category?> GetByIdAsync(Guid id) => await _dbSet.FirstOrDefaultAsync(u => u.id == id);
        public async Task CreateAsync(Category category) { _dbSet.Add(category); await _dbContext.SaveChangesAsync(); }
        public async Task UpdateAsync(Category category) { _dbSet.Update(category); await _dbContext.SaveChangesAsync(); }
        public async Task DeleteAsync(Guid id) { var u = await _dbSet.FindAsync(id); if (u != null) { _dbSet.Remove(u); await _dbContext.SaveChangesAsync(); } }

        public async Task<(IEnumerable<Category> Items, int Total)> GetPagedAsync(
            int page, int pageSize, Guid? tenantId = null, string? search = null, int? statusId = null, Guid? parentId = null, string sortField = "name", bool sortDescending = false)
        {
            var q = _dbSet.AsQueryable();
            if (tenantId.HasValue) q = q.Where(x => x.tenant_id == tenantId);
            if (!string.IsNullOrEmpty(search)) q = q.Where(x => x.name.Contains(search));
            if (statusId.HasValue) q = q.Where(x => x.status_id == statusId);
            if (parentId.HasValue) q = q.Where(x => x.parent_id == parentId);

            int total = await q.CountAsync();
            var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }
    }
}
