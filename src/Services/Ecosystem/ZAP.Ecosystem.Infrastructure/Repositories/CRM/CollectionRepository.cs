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
    public class CollectionRepository : BaseRepository<Collection>, ICollectionRepository
    {
        public CollectionRepository(EcosystemDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<Collection> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, string? search = null)
        {
            var query = _dbContext.Set<Collection>()
                .Include(x => x.status)
                .ThenInclude(s => s.translations)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(x => x.name.Contains(search));

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(x => x.name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task CreateAsync(Collection collection)
        {
            await _dbSet.AddAsync(collection);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Collection collection)
        {
            _dbContext.Entry(collection).State = EntityState.Modified;
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

        public async Task AddItemsAsync(Guid collectionId, IEnumerable<Guid> productIds)
        {
            // Implementation for collection items
            await Task.CompletedTask;
        }

        public async Task RemoveItemsAsync(Guid collectionId, IEnumerable<Guid> productIds)
        {
            await Task.CompletedTask;
        }

        Task<Collection?> ICollectionRepository.GetByIdAsync(Guid id) => GetByIdAsync(id);
    }
}
