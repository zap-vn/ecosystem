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
namespace ZAP.CRM.Catalog.Infrastructure.Repositories.Locations;
    public class LocationRepository : ZAP.Ecosystem.Shared.Data.BaseRepository<Location>, ILocationRepository
    {
        private readonly EcosystemDbContext _context;
        public LocationRepository(EcosystemDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateStoreAsync(Store store) { _context.Set<Store>().Add(store); await _context.SaveChangesAsync(); }

        public async Task<IEnumerable<GeoProvince>> GetProvincesAsync(int localeId) => await _context.Set<GeoProvince>().Include(x => x.translations).ToListAsync();

        public async Task<IEnumerable<Location>> GetPagedAsync(LocationListFilter filter)
        {
            var query = _context.Set<Location>().AsQueryable();
            if (filter.TenantId.HasValue) query = query.Where(x => x.tenant_id == filter.TenantId.Value);
            if (!string.IsNullOrEmpty(filter.Search)) query = query.Where(x => x.name.Contains(filter.Search));
            if (filter.StatusId.HasValue) query = query.Where(x => x.status_id == filter.StatusId.Value);
            return await query.OrderByDescending(x => x.created_at).Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(LocationListFilter filter)
        {
            var query = _context.Set<Location>().AsQueryable();
            if (filter.TenantId.HasValue) query = query.Where(x => x.tenant_id == filter.TenantId.Value);
            if (!string.IsNullOrEmpty(filter.Search)) query = query.Where(x => x.name.Contains(filter.Search));
            if (filter.StatusId.HasValue) query = query.Where(x => x.status_id == filter.StatusId.Value);
            return await query.CountAsync();
        }
    }



