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
using System.Threading.Tasks;
using ZAP.CRM.Catalog.Domain;
using Microsoft.EntityFrameworkCore;

using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Menus;
using ZAP.CRM.Catalog.Domain.Entities.Categories;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using ZAP.CRM.Catalog.Domain.Entities.Modifiers;
using ZAP.CRM.Catalog.Domain.Entities.Geography;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using ZAP.CRM.Catalog.Domain.Interfaces;
namespace ZAP.CRM.Catalog.Infrastructure.Repositories.Menus;
    public class MenuRepository : ZAP.Ecosystem.Shared.Data.BaseRepository<MenuHeader>, IMenuRepository
    {
        public MenuRepository(EcosystemDbContext context) : base(context) { }

    public async Task<(IEnumerable<MenuHeader> Items, int Total)> GetPagedAsync(
        int page, int pageSize, 
        Guid? tenantId = null, string? search = null, 
        bool? isActive = null, string? menuType = null, 
        int localeId = 2, string sortField = "name", bool sortDescending = false)
    {
        var query = _dbContext.Set<MenuHeader>()
            .AsNoTracking()
            .AsQueryable();

        // No tenant_id in menu_header table based on the columns check
        // if (tenantId.HasValue) query = query.Where(m => m.tenant_id == tenantId);

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(m => m.name.Contains(search));
        }

        if (isActive.HasValue)
        {
            query = query.Where(m => m.is_active == isActive.Value);
        }

        if (!string.IsNullOrEmpty(menuType))
        {
            query = query.Where(m => m.menu_type == menuType);
        }

        var total = await query.CountAsync();
        
        // Sorting
        query = sortField.ToLower() switch
        {
            "name" => sortDescending ? query.OrderByDescending(m => m.name) : query.OrderBy(m => m.name),
            _ => query.OrderBy(m => m.name)
        };

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, total);
    }
    }



