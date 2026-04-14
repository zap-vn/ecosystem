using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class ModifierGroupRepository : IModifierGroupRepository
    {
        private readonly EcosystemDbContext _context;
        public ModifierGroupRepository(EcosystemDbContext context) => _context = context;

        public async Task<IEnumerable<ModifierGroup>> GetAllAsync(Guid? tenantId = null) => await _context.ModifierGroups.Where(x => tenantId == null || x.tenant_id == tenantId).ToListAsync();
        public async Task<ModifierGroup?> GetByIdAsync(Guid id) => await _context.ModifierGroups.FirstOrDefaultAsync(u => u.id == id);
        public async Task CreateAsync(ModifierGroup mg) { _context.ModifierGroups.Add(mg); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(ModifierGroup mg) { _context.ModifierGroups.Update(mg); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(Guid id) { var u = await _context.ModifierGroups.FindAsync(id); if (u != null) { _context.ModifierGroups.Remove(u); await _context.SaveChangesAsync(); } }

        public async Task<(IEnumerable<ModifierGroup> Items, int Total)> GetPagedAsync(
            int page, int pageSize, Guid? tenantId = null, string? search = null, int? statusId = null, string? displayType = null, string sortField = "name", bool sortDescending = false)
        {
            var q = _context.ModifierGroups.AsQueryable();
            if (tenantId.HasValue) q = q.Where(x => x.tenant_id == tenantId);
            if (!string.IsNullOrEmpty(search)) q = q.Where(x => x.name.Contains(search));
            if (statusId.HasValue) q = q.Where(x => x.status_id == statusId);

            int total = await q.CountAsync();
            var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }
    }
}
