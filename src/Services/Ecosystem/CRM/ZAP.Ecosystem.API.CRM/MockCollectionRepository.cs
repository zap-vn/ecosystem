using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.API.CRM
{
    public class MockCollectionRepository : ICollectionRepository
    {
        private readonly EcosystemDbContext _context;
        public MockCollectionRepository(EcosystemDbContext context) { _context = context; }

        public Task CreateAsync(Collection collection) => Task.CompletedTask;
        public Task UpdateAsync(Collection collection) => Task.CompletedTask;
        public Task DeleteAsync(Guid id) => Task.CompletedTask;
        public Task AddItemsAsync(Guid collectionId, IEnumerable<Guid> productIds) => Task.CompletedTask;
        public Task RemoveItemsAsync(Guid collectionId, IEnumerable<Guid> productIds) => Task.CompletedTask;
        public Task<Collection?> GetByIdAsync(Guid id) => Task.FromResult<Collection?>(null);

        public async Task<(IEnumerable<Collection> Items, int TotalCount)> GetPagedAsync(
            int page, int pageSize, string? search = null)
        {
            var list = new List<Collection>();
            int total = 0;
            try
            {
                pageSize = pageSize > 0 ? pageSize : 10;
                page     = page     > 0 ? page     : 1;
                int offset = (page - 1) * pageSize;

                var conn = _context.Database.GetDbConnection();
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();

                // Count
                using (var countCmd = conn.CreateCommand())
                {
                    var where = string.IsNullOrWhiteSpace(search)
                        ? string.Empty
                        : $"WHERE name ILIKE '%{search.Replace("'", "''")}%'";
                    countCmd.CommandText = $"SELECT COUNT(*) FROM catalog.collection {where}";
                    total = Convert.ToInt32(await countCmd.ExecuteScalarAsync());
                }

                // Fetch page with product_count from collection_item
                var whereClause = string.IsNullOrWhiteSpace(search)
                    ? string.Empty
                    : $"WHERE c.name ILIKE '%{search.Replace("'", "''")}%'";

                using var cmd = conn.CreateCommand();
                cmd.CommandText = $@"
                    SELECT c.id, c.name, c.slug, c.description, c.image_url,
                           c.status_id, c.sort_order, c.created_at, c.updated_at,
                           COUNT(ci.product_id)::int AS product_count,
                           si.code AS status_code, sit.name AS status_name
                    FROM catalog.collection c
                    LEFT JOIN catalog.collection_item ci ON ci.collection_id = c.id
                    LEFT JOIN system.status_item si ON si.id = c.status_id
                    LEFT JOIN system.status_item_translation sit ON sit.status_item_id = si.id AND sit.locale_id = 2
                    {whereClause}
                    GROUP BY c.id, c.name, c.slug, c.description, c.image_url,
                             c.status_id, c.sort_order, c.created_at, c.updated_at,
                             si.code, sit.name
                    ORDER BY c.name
                    LIMIT {pageSize} OFFSET {offset}";

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var col = new Collection
                    {
                        id               = reader.IsDBNull(0)  ? Guid.Empty      : reader.GetGuid(0),
                        name             = reader.IsDBNull(1)  ? string.Empty    : reader.GetString(1),
                        slug             = reader.IsDBNull(2)  ? string.Empty    : reader.GetString(2),
                        description_html = reader.IsDBNull(3)  ? null            : reader.GetString(3),
                        banner_url       = reader.IsDBNull(4)  ? null            : reader.GetString(4),
                        status_id        = reader.IsDBNull(5)  ? 0               : reader.GetInt32(5),
                        sort_order       = reader.IsDBNull(6)  ? 0               : reader.GetInt32(6),
                        created_at       = reader.IsDBNull(7)  ? DateTime.UtcNow : reader.GetDateTime(7),
                        updated_at       = reader.IsDBNull(8)  ? null            : reader.GetDateTime(8),
                        status_code      = reader.IsDBNull(10) ? null            : reader.GetString(10),
                        status_name      = reader.IsDBNull(11) ? null            : reader.GetString(11),
                    };
                    // Store product_count via items count proxy
                    int productCount = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                    for (int i = 0; i < productCount; i++)
                        col.items.Add(new CollectionItem { collection_id = col.id });

                    list.Add(col);
                }
            }
            catch (Exception ex)
            {
                list.Add(new Collection { id = Guid.NewGuid(), name = "DEBUG SQL ERROR: " + ex.Message });
            }
            return (list, total);
        }
    }
}
