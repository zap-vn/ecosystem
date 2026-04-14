using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcosystemDbContext _context;
        public ProductRepository(EcosystemDbContext context) => _context = context;

        public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.ToListAsync();
        public async Task<Product?> GetByIdAsync(string id) => await _context.Products.FirstOrDefaultAsync(p => p.id == Guid.Parse(id));
        public async Task CreateAsync(Product product) { _context.Products.Add(product); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Product product) { _context.Products.Update(product); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(string id) { var p = await _context.Products.FindAsync(Guid.Parse(id)); if (p != null) { _context.Products.Remove(p); await _context.SaveChangesAsync(); } }

        public async Task<(IEnumerable<ProductVariant> Items, int TotalCount)> GetPagedAsync(
            int page, int pageSize, Guid? tenantId = null, string? searchTerm = null, int? statusId = null, Guid? categoryId = null, Guid? locationId = null, int localeId = 2, int? productTypeId = null, string sortField = "created_at", bool sortDescending = true)
        {
            var q = _context.ProductVariants.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm)) q = q.Where(x => x.variant_name != null && x.variant_name.Contains(searchTerm));
            int total = await q.CountAsync();
            var items = await q.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }

        public async Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedProductsAsync(
            int page, int pageSize, Guid? tenantId = null, string? searchTerm = null, int? statusId = null, Guid? categoryId = null, Guid? locationId = null, int localeId = 2, int? productTypeId = null, string sortField = "created_at", bool sortDescending = true)
        {
            var query = _context.Products
                .Include(p => p.status).ThenInclude(s => s.translations)
                .Include(p => p.product_type).ThenInclude(pt => pt.translations)
                .Include(p => p.category_mappings).ThenInclude(cm => cm.category)
                .Include(p => p.variants).ThenInclude(v => v.media)
                .Include(p => p.variants).ThenInclude(v => v.inventory_items)
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
}
