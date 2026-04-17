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
namespace ZAP.CRM.Catalog.Infrastructure.Repositories.Products;
    public class ProductRepository : ZAP.Ecosystem.Shared.Data.BaseRepository<Product>, IProductRepository
    {
        private readonly EcosystemDbContext _context;
        public ProductRepository(EcosystemDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<ProductVariant> Items, int TotalCount)> GetPagedAsync(
            int page, int pageSize, Guid? tenantId = null, string? searchTerm = null, int? statusId = null, Guid? categoryId = null, Guid? locationId = null, int localeId = 2, int? productTypeId = null, string sortField = "created_at", bool sortDescending = true)
        {
            var q = _context.Set<ProductVariant>().AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm)) q = q.Where(x => x.variant_name != null && x.variant_name.Contains(searchTerm));
            int total = await q.CountAsync();
            var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }

        public async Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedProductsAsync(
            int page, int pageSize, Guid? tenantId = null, string? searchTerm = null, int? statusId = null, Guid? categoryId = null, Guid? locationId = null, int localeId = 2, int? productTypeId = null, string sortField = "created_at", bool sortDescending = true)
        {
            var query = _context.Set<Product>()
                .Include(p => p.status).ThenInclude(s => s.translations)
                .Include(p => p.category).ThenInclude(c => c.translations)
                .Include(p => p.translations)
                .Include(p => p.variants)
                .Include(p => p.variants).ThenInclude(v => v.location_pricing)
                .AsQueryable();

            if (tenantId.HasValue) query = query.Where(p => p.tenant_id == tenantId.Value);
            if (!string.IsNullOrEmpty(searchTerm)) query = query.Where(p => p.name.Contains(searchTerm));
            if (statusId.HasValue) query = query.Where(p => p.status_id == statusId.Value);
            if (categoryId.HasValue) query = query.Where(p => p.category_mappings.Any(c => c.category_id == categoryId.Value));
            if (productTypeId.HasValue) query = query.Where(p => p.product_type_id == productTypeId.Value);

            int total = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }
    }



