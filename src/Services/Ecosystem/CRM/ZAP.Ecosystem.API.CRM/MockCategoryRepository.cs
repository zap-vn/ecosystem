using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.API.CRM
{
    public class MockCategoryRepository : ICategoryRepository
    {
        private readonly EcosystemDbContext _context;
        public MockCategoryRepository(EcosystemDbContext context) { _context = context; }

        public Task CreateAsync(Category category) => Task.CompletedTask;
        public Task UpdateAsync(Category category) => Task.CompletedTask;
        public Task DeleteAsync(Guid id) => Task.CompletedTask;
        public Task<Category?> GetByIdAsync(Guid id) => Task.FromResult<Category?>(null);

        public async Task<IEnumerable<Category>> GetAllAsync(Guid? tenantId = null)
        {
            var (items, _) = await GetPagedAsync(1, 1000, tenantId);
            return items;
        }

        public async Task<(IEnumerable<Category> Items, int Total)> GetPagedAsync(
            int page, int pageSize,
            Guid? tenantId = null, string? search = null,
            int? statusId = null, Guid? parentId = null,
            string sortField = "name", bool sortDescending = false)
        {
            var list = new List<Category>();
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
                if (!string.IsNullOrWhiteSpace(search))
                    clauses.Add($"c.name ILIKE '%{search.Replace("'", "''")}%'");
                if (statusId.HasValue)
                    clauses.Add($"c.status_id = {statusId.Value}");
                if (parentId.HasValue)
                    clauses.Add($"c.parent_id = '{parentId.Value}'");
                var where = clauses.Count > 0 ? "WHERE " + string.Join(" AND ", clauses) : "";

                var sort = sortField switch
                {
                    "sort_order" => "c.sort_order",
                    "status_id"  => "c.status_id",
                    _            => "c.name"
                };

                using (var countCmd = conn.CreateCommand())
                {
                    countCmd.CommandText = $"SELECT COUNT(*) FROM catalog.category c {where}";
                    total = Convert.ToInt32(await countCmd.ExecuteScalarAsync());
                }

                using var cmd = conn.CreateCommand();
                cmd.CommandText = $@"
                    SELECT c.id, c.tenant_id, c.parent_id, c.serial_id, c.legacy_id,
                           c.name, c.slug, c.icon_url, c.banner_url, c.sort_order,
                           c.status_id, c.is_active, c.canonical_url,
                           si.code AS status_code, sit.name AS status_name
                    FROM catalog.category c
                    LEFT JOIN system.status_item si ON si.id = c.status_id
                    LEFT JOIN system.status_item_translation sit ON sit.status_item_id = si.id AND sit.locale_id = 2
                    {where}
                    ORDER BY {sort} {(sortDescending ? "DESC" : "ASC")}
                    LIMIT {pageSize} OFFSET {offset}";

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    list.Add(new Category
                    {
                        id            = reader.IsDBNull(0)  ? Guid.Empty   : reader.GetGuid(0),
                        tenant_id     = reader.IsDBNull(1)  ? null         : reader.GetGuid(1),
                        parent_id     = reader.IsDBNull(2)  ? null         : reader.GetGuid(2),
                        serial_id     = reader.IsDBNull(3)  ? null         : reader.GetInt32(3),
                        legacy_id     = reader.IsDBNull(4)  ? null         : reader.GetString(4),
                        name          = reader.IsDBNull(5)  ? string.Empty : reader.GetString(5),
                        slug          = reader.IsDBNull(6)  ? null         : reader.GetString(6),
                        icon_url      = reader.IsDBNull(7)  ? null         : reader.GetString(7),
                        banner_url    = reader.IsDBNull(8)  ? null         : reader.GetString(8),
                        sort_order    = reader.IsDBNull(9)  ? null         : reader.GetInt32(9),
                        status_id     = reader.IsDBNull(10) ? null         : reader.GetInt32(10),
                        is_active     = !reader.IsDBNull(11) && reader.GetBoolean(11),
                        canonical_url = reader.IsDBNull(12) ? null         : reader.GetString(12),
                        status_code   = reader.IsDBNull(13) ? null         : reader.GetString(13),
                        status_name   = reader.IsDBNull(14) ? null         : reader.GetString(14),
                    });
                }
            }
            catch (Exception ex)
            {
                list.Add(new Category { id = Guid.NewGuid(), name = "DEBUG SQL ERROR: " + ex.Message });
            }
            return (list, total);
        }
    }
}
