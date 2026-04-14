using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class BrandRepository : IBrandRepository
    {
        private readonly EcosystemDbContext _context;
        public BrandRepository(EcosystemDbContext context) => _context = context;

        public async Task<IEnumerable<Brand>> GetAllAsync(Guid? tenantId = null) => await _context.Brands.Where(x => tenantId == null || x.tenant_id == tenantId).ToListAsync();
        public async Task<Brand?> GetByIdAsync(Guid id) => await _context.Brands.FirstOrDefaultAsync(u => u.id == id);
        public async Task CreateAsync(Brand brand) { _context.Brands.Add(brand); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Brand brand) { _context.Brands.Update(brand); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(Guid id) { var u = await _context.Brands.FindAsync(id); if (u != null) { _context.Brands.Remove(u); await _context.SaveChangesAsync(); } }

        public async Task<(IEnumerable<Brand> Items, int Total)> GetPagedAsync(
            int page, int pageSize, Guid? tenantId = null, string? search = null, int? statusId = null, string sortField = "name", bool sortDescending = false)
        {
            var q = _context.Brands.AsQueryable();
            if (tenantId.HasValue) q = q.Where(x => x.tenant_id == tenantId);
            if (!string.IsNullOrEmpty(search)) q = q.Where(x => x.name.Contains(search));
            if (statusId.HasValue) q = q.Where(x => x.status_id == statusId);

            int total = await q.CountAsync();
            var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }
    }
}
