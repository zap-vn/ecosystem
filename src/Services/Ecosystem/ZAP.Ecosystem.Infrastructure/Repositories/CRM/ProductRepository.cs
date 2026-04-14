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
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(EcosystemDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(string id)
        {
            if (Guid.TryParse(id, out var guid))
                return await _dbSet.FirstOrDefaultAsync(p => p.id == guid);
            return null;
        }

        public async Task CreateAsync(Product product)
        {
            await _dbSet.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<(IEnumerable<ProductVariant> Items, int TotalCount)> GetPagedAsync(
            int page, int pageSize,
            Guid? tenantId = null, string? searchTerm = null,
            int? statusId = null, Guid? categoryId = null,
            Guid? locationId = null, int localeId = 2,
            int? productTypeId = null, string sortField = "created_at",
            bool sortDescending = true)
        {
            // Implementation for Variant paging
            return await Task.FromResult((new List<ProductVariant>() as IEnumerable<ProductVariant>, 0));
        }

        public async Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedProductsAsync(
            int page, int pageSize,
            Guid? tenantId = null, string? searchTerm = null,
            int? statusId = null, Guid? categoryId = null,
            Guid? locationId = null, int localeId = 2,
            int? productTypeId = null, string sortField = "created_at",
            bool sortDescending = true)
        {
            var query = _dbSet
                .Include(p => p.status)
                .ThenInclude(s => s.translations)
                .Include(p => p.product_type)
                .AsNoTracking();

            if (tenantId.HasValue) query = query.Where(p => p.tenant_id == tenantId.Value);
            if (statusId.HasValue) query = query.Where(p => p.status_id == statusId.Value);
            if (productTypeId.HasValue) query = query.Where(p => p.product_type_id == productTypeId.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(p => p.name.Contains(searchTerm));

            var totalCount = await query.CountAsync();

            query = sortField.ToLower() switch
            {
                "name" => sortDescending ? query.OrderByDescending(p => p.name) : query.OrderBy(p => p.name),
                "created_at" => sortDescending ? query.OrderByDescending(p => p.created_at) : query.OrderBy(p => p.created_at),
                _ => sortDescending ? query.OrderByDescending(p => p.created_at) : query.OrderBy(p => p.created_at)
            };

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
