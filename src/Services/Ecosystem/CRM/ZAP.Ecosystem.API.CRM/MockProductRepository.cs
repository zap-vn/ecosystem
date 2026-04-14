using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.API.CRM
{
    public class MockProductRepository : IProductRepository
    {
        private readonly EcosystemDbContext _context;
        public MockProductRepository(EcosystemDbContext context) { _context = context; }

        public Task CreateAsync(Product product) => Task.CompletedTask;
        public Task UpdateAsync(Product product) => Task.CompletedTask;
        public Task DeleteAsync(string id) => Task.CompletedTask;
        public Task<Product?> GetByIdAsync(string id) => Task.FromResult<Product?>(null);
        public Task<IEnumerable<Product>> GetAllAsync() => Task.FromResult<IEnumerable<Product>>(new List<Product>());

        public async Task<(IEnumerable<ProductVariant> Items, int TotalCount)> GetPagedAsync(
            int page, int pageSize,
            Guid? tenantId = null, string? searchTerm = null,
            int? statusId = null, Guid? categoryId = null, Guid? locationId = null,
            int localeId = 2, int? productTypeId = null,
            string sortField = "created_at", bool sortDescending = true)
        {
            // Delegate to GetPagedProductsAsync and wrap first variant
            var (products, total) = await GetPagedProductsAsync(page, pageSize, tenantId, searchTerm,
                statusId, categoryId, locationId, localeId, productTypeId, sortField, sortDescending);
            var variants = new List<ProductVariant>();
            foreach (var p in products)
                foreach (var v in p.variants)
                    variants.Add(v);
            return (variants, total);
        }

        public async Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedProductsAsync(
            int page, int pageSize,
            Guid? tenantId = null, string? searchTerm = null,
            int? statusId = null, Guid? categoryId = null, Guid? locationId = null,
            int localeId = 2, int? productTypeId = null,
            string sortField = "created_at", bool sortDescending = true)
        {
            var list = new List<Product>();
            int total = 0;
            try
            {
                pageSize = pageSize > 0 ? pageSize : 10;
                page     = page     > 0 ? page     : 1;
                int offset = (page - 1) * pageSize;

                var conn = _context.Database.GetDbConnection();
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();

                var clauses = new List<string>();
                if (!string.IsNullOrWhiteSpace(searchTerm))
                    clauses.Add($"p.name ILIKE '%{searchTerm.Replace("'", "''")}%'");
                if (statusId.HasValue)
                    clauses.Add($"p.status_id = {statusId.Value}");
                if (productTypeId.HasValue)
                    clauses.Add($"p.product_type_id = {productTypeId.Value}");
                var where = clauses.Count > 0 ? "WHERE " + string.Join(" AND ", clauses) : "";

                var sort = sortField switch
                {
                    "name"       => "p.name",
                    "status_id"  => "p.status_id",
                    _            => "p.created_at"
                };

                using (var countCmd = conn.CreateCommand())
                {
                    countCmd.CommandText = $"SELECT COUNT(*) FROM catalog.product p {where}";
                    total = Convert.ToInt32(await countCmd.ExecuteScalarAsync());
                }

                using var cmd = conn.CreateCommand();
                cmd.CommandText = $@"
                    SELECT p.id, p.serial_id, p.tenant_id, p.legacy_id,
                           p.product_type_id, p.name, p.short_description,
                           p.status_id, p.is_featured, p.created_at, p.updated_at,
                           v.id, v.sku_code, v.barcode, v.variant_name,
                           v.base_price, v.sale_price, v.is_default
                    FROM catalog.product p
                    LEFT JOIN catalog.product_variant v ON v.product_id = p.id AND v.is_default = true
                    {where}
                    ORDER BY {sort} {(sortDescending ? "DESC" : "ASC")}
                    LIMIT {pageSize} OFFSET {offset}";

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var product = new Product
                    {
                        id              = reader.IsDBNull(0)  ? Guid.Empty    : reader.GetGuid(0),
                        serial_id       = reader.IsDBNull(1)  ? null          : reader.GetInt32(1),
                        tenant_id       = reader.IsDBNull(2)  ? null          : reader.GetGuid(2),
                        legacy_id       = reader.IsDBNull(3)  ? null          : reader.GetString(3),
                        product_type_id = reader.IsDBNull(4)  ? 1             : reader.GetInt32(4),
                        name            = reader.IsDBNull(5)  ? string.Empty  : reader.GetString(5),
                        short_description = reader.IsDBNull(6) ? null         : reader.GetString(6),
                        status_id       = reader.IsDBNull(7)  ? null          : reader.GetInt32(7),
                        is_featured     = !reader.IsDBNull(8) && reader.GetBoolean(8),
                        created_at      = reader.IsDBNull(9)  ? DateTime.UtcNow : reader.GetDateTime(9),
                        updated_at      = reader.IsDBNull(10) ? null          : reader.GetDateTime(10),
                    };

                    if (!reader.IsDBNull(11))
                    {
                        product.variants.Add(new ProductVariant
                        {
                            id           = reader.GetGuid(11),
                            product_id   = product.id,
                            sku_code     = reader.IsDBNull(12) ? null : reader.GetString(12),
                            barcode      = reader.IsDBNull(13) ? null : reader.GetString(13),
                            variant_name = reader.IsDBNull(14) ? null : reader.GetString(14),
                            base_price   = reader.IsDBNull(15) ? null : reader.GetDecimal(15),
                            sale_price   = reader.IsDBNull(16) ? null : reader.GetDecimal(16),
                            is_default   = !reader.IsDBNull(17) && reader.GetBoolean(17),
                        });
                    }

                    list.Add(product);
                }
            }
            catch (Exception ex)
            {
                list.Add(new Product { id = Guid.NewGuid(), name = "DEBUG SQL ERROR: " + ex.Message });
            }
            return (list, total);
        }
    }
}
