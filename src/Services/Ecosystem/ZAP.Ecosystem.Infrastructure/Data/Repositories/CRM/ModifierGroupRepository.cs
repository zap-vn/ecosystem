using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class ModifierGroupRepository : ZAP.Ecosystem.Shared.Data.BaseRepository<ModifierGroup>, IModifierGroupRepository
    {
        public ModifierGroupRepository(EcosystemDbContext context) : base(context) { }

        public async Task<IEnumerable<ModifierGroup>> GetAllAsync(Guid? tenantId = null) => await _dbContext.Set<ModifierGroup>().Where(x => tenantId == null || x.tenant_id == tenantId).ToListAsync();
        public async Task<ModifierGroup?> GetByIdAsync(Guid id) => await _dbContext.Set<ModifierGroup>().FirstOrDefaultAsync(u => u.id == id);
        public async Task CreateAsync(ModifierGroup mg) { _dbContext.Set<ModifierGroup>().Add(mg); await _dbContext.SaveChangesAsync(); }
        public async Task UpdateAsync(ModifierGroup mg) { _dbContext.Set<ModifierGroup>().Update(mg); await _dbContext.SaveChangesAsync(); }
        public async Task DeleteAsync(Guid id) { var u = await _dbContext.Set<ModifierGroup>().FindAsync(id); if (u != null) { _dbContext.Set<ModifierGroup>().Remove(u); await _dbContext.SaveChangesAsync(); } }

        public async Task<(IEnumerable<ModifierGroup> Items, int Total)> GetPagedAsync(
            int page, int pageSize, Guid? tenantId = null, string? search = null, int? statusId = null, string? displayType = null, string sortField = "name", bool sortDescending = false)
        {
            var q = _dbContext.Set<ModifierGroup>().AsQueryable();
            if (tenantId.HasValue) q = q.Where(x => x.tenant_id == tenantId);
            if (!string.IsNullOrEmpty(search)) q = q.Where(x => x.name.Contains(search));
            if (statusId.HasValue) q = q.Where(x => x.status_id == statusId);

            int total = await q.CountAsync();
            var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }
    }
}
