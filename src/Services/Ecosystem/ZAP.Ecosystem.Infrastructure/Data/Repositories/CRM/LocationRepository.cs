using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class LocationRepository : ILocationRepository
    {
        private readonly EcosystemDbContext _context;
        public LocationRepository(EcosystemDbContext context) => _context = context;

        public async Task<Location?> GetByIdAsync(Guid id) => await _context.Locations.FirstOrDefaultAsync(l => l.id == id);
        public async Task CreateAsync(Location location) { _context.Locations.Add(location); await _context.SaveChangesAsync(); }
        public async Task CreateStoreAsync(Store store) { _context.Stores.Add(store); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Location location) { _context.Locations.Update(location); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(Guid id) { var l = await _context.Locations.FindAsync(id); if (l != null) { _context.Locations.Remove(l); await _context.SaveChangesAsync(); } }

        public async Task<IEnumerable<GeoProvince>> GetProvincesAsync(int localeId) => await _context.GeoProvinces.Include(x => x.translations).ToListAsync();

        public async Task<IEnumerable<Location>> GetPagedAsync(ZAP.Ecosystem.Domain.CRM.LocationListFilter filter)
        {
            var query = _context.Locations.AsQueryable();
            if (filter.TenantId.HasValue) query = query.Where(x => x.tenant_id == filter.TenantId.Value);
            if (!string.IsNullOrEmpty(filter.Search)) query = query.Where(x => x.name.Contains(filter.Search));
            if (filter.StatusId.HasValue) query = query.Where(x => x.status_id == filter.StatusId.Value);
            return await query.OrderByDescending(x => x.created_at).Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(ZAP.Ecosystem.Domain.CRM.LocationListFilter filter)
        {
            var query = _context.Locations.AsQueryable();
            if (filter.TenantId.HasValue) query = query.Where(x => x.tenant_id == filter.TenantId.Value);
            if (!string.IsNullOrEmpty(filter.Search)) query = query.Where(x => x.name.Contains(filter.Search));
            if (filter.StatusId.HasValue) query = query.Where(x => x.status_id == filter.StatusId.Value);
            return await query.CountAsync();
        }
    }
}
