using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class UnitRepository : IUnitRepository
    {
        private readonly EcosystemDbContext _context;
        public UnitRepository(EcosystemDbContext context) => _context = context;

        public async Task<IEnumerable<UomItem>> GetAllAsync(Guid? tenantId = null) => await _context.UomItems.Where(x => tenantId == null || x.tenant_id == tenantId).ToListAsync();
        public async Task<UomItem?> GetByIdAsync(int id) => await _context.UomItems.FirstOrDefaultAsync(u => u.id == id);
        public async Task CreateAsync(UomItem uomItem) { _context.UomItems.Add(uomItem); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(UomItem uomItem) { _context.UomItems.Update(uomItem); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id) { var u = await _context.UomItems.FindAsync(id); if (u != null) { _context.UomItems.Remove(u); await _context.SaveChangesAsync(); } }

        public async Task<(IEnumerable<UomItem> Items, int Total)> GetPagedAsync(
            int page, int pageSize, Guid? tenantId = null, string? search = null, int? statusId = null, int? precision = null, string sortField = "name", bool sortDescending = false)
        {
            var q = _context.UomItems.AsQueryable();
            if (tenantId.HasValue) q = q.Where(x => x.tenant_id == tenantId);
            if (!string.IsNullOrEmpty(search)) q = q.Where(x => x.name.Contains(search));
            if (statusId.HasValue) q = q.Where(x => x.status_id == statusId);
            if (precision.HasValue) q = q.Where(x => x.precision == precision);

            int total = await q.CountAsync();
            var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }
    }
}
