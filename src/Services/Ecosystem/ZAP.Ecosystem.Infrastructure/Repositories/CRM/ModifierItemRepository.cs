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
    public class ModifierItemRepository : BaseRepository<ModifierItem>, IModifierItemRepository
    {
        public ModifierItemRepository(EcosystemDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ModifierItem>> GetAllAsync(Guid? groupId = null)
        {
            var query = _dbSet.AsNoTracking();
            if (groupId.HasValue) query = query.Where(i => i.group_id == groupId.Value);
            return await query.ToListAsync();
        }

        public async Task<(IEnumerable<ModifierItem> Items, int Total)> GetPagedAsync(
            int page, int pageSize,
            Guid? groupId = null, int? statusId = null,
            string sortField = "sort_order", bool sortDescending = false)
        {
            var query = _dbSet.AsNoTracking();

            if (groupId.HasValue) query = query.Where(i => i.group_id == groupId.Value);
            if (statusId.HasValue) query = query.Where(i => i.status_id == statusId.Value);

            var totalCount = await query.CountAsync();

            query = sortField.ToLower() switch
            {
                "sort_order" => sortDescending ? query.OrderByDescending(i => i.sort_order) : query.OrderBy(i => i.sort_order),
                _ => sortDescending ? query.OrderByDescending(i => i.sort_order) : query.OrderBy(i => i.sort_order)
            };

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        Task<ModifierItem?> IModifierItemRepository.GetByIdAsync(Guid id) => GetByIdAsync(id);
    }
}
