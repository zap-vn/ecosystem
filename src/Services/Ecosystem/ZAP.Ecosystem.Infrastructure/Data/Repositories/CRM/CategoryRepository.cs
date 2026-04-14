using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EcosystemDbContext _context;
        public CategoryRepository(EcosystemDbContext context) => _context = context;

        public async Task<IEnumerable<Category>> GetAllAsync(Guid? tenantId = null) => await _context.Categories.Where(x => tenantId == null || x.tenant_id == tenantId).ToListAsync();
        public async Task<Category?> GetByIdAsync(Guid id) => await _context.Categories.FirstOrDefaultAsync(u => u.id == id);
        public async Task CreateAsync(Category category) { _context.Categories.Add(category); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Category category) { _context.Categories.Update(category); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(Guid id) { var u = await _context.Categories.FindAsync(id); if (u != null) { _context.Categories.Remove(u); await _context.SaveChangesAsync(); } }

        public async Task<(IEnumerable<Category> Items, int Total)> GetPagedAsync(
            int page, int pageSize, Guid? tenantId = null, string? search = null, int? statusId = null, Guid? parentId = null, string sortField = "name", bool sortDescending = false)
        {
            var q = _context.Categories.AsQueryable();
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
