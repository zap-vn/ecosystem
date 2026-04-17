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
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAP.CRM.Catalog.Domain;

using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Menus;
using ZAP.CRM.Catalog.Domain.Entities.Categories;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using ZAP.CRM.Catalog.Domain.Entities.Modifiers;
using ZAP.CRM.Catalog.Domain.Entities.Geography;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using ZAP.CRM.Catalog.Domain.Interfaces;
namespace ZAP.CRM.Catalog.Infrastructure.Repositories.Categories;
    public class CategoryRepository : ZAP.Ecosystem.Shared.Data.BaseRepository<Category>, ICategoryRepository
    {
        private readonly EcosystemDbContext _context;
        public CategoryRepository(EcosystemDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(Guid? tenantId = null) => await _context.Set<Category>().Where(x => tenantId == null || x.tenant_id == tenantId).ToListAsync();

        public async Task<(IEnumerable<Category> Items, int Total)> GetPagedAsync(
            int page, int pageSize, Guid? tenantId = null, string? search = null, int? statusId = null, Guid? parentId = null, string sortField = "name", bool sortDescending = false)
        {
            var q = _context.Set<Category>().AsQueryable();
            if (tenantId.HasValue) q = q.Where(x => x.tenant_id == tenantId);
            if (!string.IsNullOrEmpty(search)) q = q.Where(x => x.name.Contains(search));
            if (statusId.HasValue) q = q.Where(x => x.status_id == statusId);
            if (parentId.HasValue) q = q.Where(x => x.parent_id == parentId);

            int total = await q.CountAsync();
            var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }
    }



