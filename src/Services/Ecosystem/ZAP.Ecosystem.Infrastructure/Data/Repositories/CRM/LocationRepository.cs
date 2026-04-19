using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class LocationRepository : ZAP.Ecosystem.Shared.Data.BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(EcosystemDbContext context) : base(context) { }

        public async Task<Location?> GetByIdAsync(Guid id) => await _dbSet.FirstOrDefaultAsync(l => l.id == id);
        public async Task CreateAsync(Location location) { _dbSet.Add(location); await _dbContext.SaveChangesAsync(); }
        public async Task CreateStoreAsync(Store store) { _dbContext.Set<Store>().Add(store); await _dbContext.SaveChangesAsync(); }
        public async Task UpdateAsync(Location location) { _dbSet.Update(location); await _dbContext.SaveChangesAsync(); }
        public async Task DeleteAsync(Guid id) { var l = await _dbSet.FindAsync(id); if (l != null) { _dbSet.Remove(l); await _dbContext.SaveChangesAsync(); } }

        public async Task<IEnumerable<GeoProvince>> GetProvincesAsync(int localeId) => await _dbContext.Set<GeoProvince>().Include(x => x.translations).ToListAsync();

        public async Task<IEnumerable<Location>> GetPagedAsync(ZAP.Ecosystem.Domain.CRM.LocationListFilter filter)
        {
            var query = _dbSet.AsQueryable();
            if (filter.TenantId.HasValue) query = query.Where(x => x.tenant_id == filter.TenantId.Value);
            if (!string.IsNullOrEmpty(filter.Search)) query = query.Where(x => x.name.Contains(filter.Search));
            if (filter.StatusId.HasValue) query = query.Where(x => x.status_id == filter.StatusId.Value);
            return await query.OrderByDescending(x => x.created_at).Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(ZAP.Ecosystem.Domain.CRM.LocationListFilter filter)
        {
            var query = _dbSet.AsQueryable();
            if (filter.TenantId.HasValue) query = query.Where(x => x.tenant_id == filter.TenantId.Value);
            if (!string.IsNullOrEmpty(filter.Search)) query = query.Where(x => x.name.Contains(filter.Search));
            if (filter.StatusId.HasValue) query = query.Where(x => x.status_id == filter.StatusId.Value);
            return await query.CountAsync();
        }
    }
}
