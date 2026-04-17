using ZAP.CRM.Catalog.Domain.Interfaces.Brands;
using ZAP.CRM.Catalog.Domain.Interfaces.Products;
using ZAP.CRM.Catalog.Domain.Interfaces.Menus;
using ZAP.CRM.Catalog.Domain.Interfaces.Categories;
using ZAP.CRM.Catalog.Domain.Interfaces.Locations;
using ZAP.CRM.Catalog.Domain.Interfaces.Modifiers;
using ZAP.CRM.Catalog.Domain.Interfaces.Geography;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using ZAP.Ecosystem.Shared.Entities;
using ZAP.Ecosystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZAP.CRM.Catalog.Domain;
using ZAP.Ecosystem.Shared.Data;

using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Menus;
using ZAP.CRM.Catalog.Domain.Entities.Categories;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using ZAP.CRM.Catalog.Domain.Entities.Modifiers;
using ZAP.CRM.Catalog.Domain.Entities.Geography;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using ZAP.CRM.Catalog.Domain.Interfaces;
namespace ZAP.CRM.Catalog.Infrastructure.Repositories.Modifiers;
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
    }



