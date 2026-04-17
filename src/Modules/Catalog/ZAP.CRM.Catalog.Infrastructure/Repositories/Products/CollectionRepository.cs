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
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
namespace ZAP.CRM.Catalog.Infrastructure.Repositories.Products;
    public class CollectionRepository : ZAP.Ecosystem.Shared.Data.BaseRepository<Collection>, ICollectionRepository
    {
        private readonly EcosystemDbContext _context;
        public CollectionRepository(EcosystemDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Collection> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, Guid? tenantId = null, string? search = null)
        {
            var query = _context.Set<Collection>().AsQueryable();
            if (tenantId.HasValue) query = query.Where(x => x.tenant_id == tenantId.Value);
            if (!string.IsNullOrEmpty(search)) query = query.Where(x => x.name.Contains(search));
            int total = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }

        public Task AddItemsAsync(Guid collectionId, IEnumerable<Guid> productIds) => throw new NotImplementedException();
        public Task RemoveItemsAsync(Guid collectionId, IEnumerable<Guid> productIds) => throw new NotImplementedException();
    }



