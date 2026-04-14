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
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(EcosystemDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Location>> GetPagedAsync(LocationListFilter filter)
        {
            var query = BuildQuery(filter);

            if (filter.SortDescending)
            {
                query = filter.SortField?.ToLower() switch
                {
                    "name" => query.OrderByDescending(l => l.name),
                    _ => query.OrderByDescending(l => l.name)
                };
            }
            else
            {
                query = filter.SortField?.ToLower() switch
                {
                    "name" => query.OrderBy(l => l.name),
                    _ => query.OrderBy(l => l.name)
                };
            }

            return await query
                .Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(LocationListFilter filter)
        {
            return await BuildQuery(filter).CountAsync();
        }

        private IQueryable<Location> BuildQuery(LocationListFilter filter)
        {
            var query = _dbSet
                .Include(l => l.status)
                .ThenInclude(s => s.translations)
                .Include(l => l.location_type)
                .ThenInclude(t => t.translations)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                query = query.Where(l => l.name.Contains(filter.Search) || l.business_name.Contains(filter.Search));
            }

            if (filter.StatusId.HasValue)
            {
                query = query.Where(l => l.status_id == filter.StatusId.Value);
            }

            if (filter.ProvinceId.HasValue)
            {
                query = query.Where(l => l.province_id == filter.ProvinceId.Value);
            }

            if (filter.LocationTypeIds != null && filter.LocationTypeIds.Any())
            {
                query = query.Where(l => filter.LocationTypeIds.Contains(l.location_type_id ?? 0));
            }

            return query;
        }

        public override async Task<Location?> GetByIdAsync(object id, System.Threading.CancellationToken cancellationToken = default)
        {
            if (id is Guid guid)
                return await _dbSet.Include(l => l.status).FirstOrDefaultAsync(l => l.id == guid, cancellationToken);
            return await base.GetByIdAsync(id, cancellationToken);
        }

        public async Task CreateAsync(Location location)
        {
            await _dbSet.AddAsync(location);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateStoreAsync(Store store)
        {
            // Implementation for Store mapping if needed
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Location location)
        {
            _dbContext.Entry(location).State = EntityState.Modified;
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

        public async Task<IEnumerable<GeoProvince>> GetProvincesAsync(int localeId)
        {
            // This might need a separate repository or stay here
            return await Task.FromResult(new List<GeoProvince>());
        }

        Task<Location?> ILocationRepository.GetByIdAsync(Guid id) => GetByIdAsync(id);
    }
}
