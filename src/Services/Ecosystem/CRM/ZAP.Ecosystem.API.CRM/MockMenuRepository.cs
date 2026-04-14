using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.API.CRM
{
    public class MockMenuRepository : IMenuRepository
    {
        private readonly EcosystemDbContext _context;
        public MockMenuRepository(EcosystemDbContext context) { _context = context; }

        public Task<MenuHeader?> GetByIdAsync(Guid id) => Task.FromResult<MenuHeader?>(null);

        public async Task<(IEnumerable<MenuHeader> Items, int Total)> GetPagedAsync(
            int page, int pageSize,
            Guid? tenantId = null, string? search = null,
            bool? isActive = null, string? menuType = null,
            int localeId = 2,
            string sortField = "name", bool sortDescending = false)
        {
            var list = new List<MenuHeader>();
            int total = 0;
            try
            {
                pageSize = pageSize > 0 ? pageSize : 10;
                page     = page     > 0 ? page     : 1;
                int offset = (page - 1) * pageSize;

                var clauses = new List<string>();
                if (tenantId.HasValue) clauses.Add($"m.tenant_id = '{tenantId.Value}'");
                if (!string.IsNullOrWhiteSpace(search)) clauses.Add($"m.name ILIKE '%{search.Replace("'", "''")}%'");
                if (isActive.HasValue) clauses.Add($"m.is_active = {isActive.Value}");
                if (!string.IsNullOrWhiteSpace(menuType)) clauses.Add($"m.menu_type = '{menuType.Replace("'", "''")}'");
                
                string where = clauses.Count > 0 ? "WHERE " + string.Join(" AND ", clauses) : "";

                var conn = _context.Database.GetDbConnection();
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();

                // Count
                using (var countCmd = conn.CreateCommand())
                {
                    countCmd.CommandText = $"SELECT COUNT(*) FROM catalog.menu m {where}";
                    total = Convert.ToInt32(await countCmd.ExecuteScalarAsync());
                }

                // Fetch page
                using var cmd = conn.CreateCommand();
                cmd.CommandText = $@"
                    SELECT m.id, m.tenant_id, m.name, m.menu_type, m.app_id, m.status_id, m.timezone_id, m.is_active,
                           si.code AS status_code, sit.name AS status_name, m.serial_id
                    FROM catalog.menu m
                    LEFT JOIN system.status_item si ON si.id = m.status_id
                    LEFT JOIN system.status_item_translation sit ON sit.status_item_id = si.id AND sit.locale_id = {localeId}
                    {where}
                    ORDER BY {EscapeSort(sortField)} {(sortDescending ? "DESC" : "ASC")}
                    LIMIT {pageSize} OFFSET {offset}";

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    list.Add(new MenuHeader
                    {
                        id          = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                        tenant_id   = reader.IsDBNull(1) ? null : reader.GetGuid(1),
                        name        = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        menu_type   = reader.IsDBNull(3) ? "DIGITAL" : reader.GetString(3),
                        app_id      = reader.IsDBNull(4) ? null : reader.GetGuid(4),
                        status_id   = reader.IsDBNull(5) ? null : reader.GetInt32(5),
                        timezone_id = reader.IsDBNull(6) ? null : reader.GetString(6),
                        is_active   = reader.IsDBNull(7) ? false : reader.GetBoolean(7),
                        status_code = reader.IsDBNull(8) ? null : reader.GetString(8),
                        status_name = reader.IsDBNull(9) ? null : reader.GetString(9),
                        serial_id   = reader.IsDBNull(10) ? (int?)null : reader.GetInt32(10),
                    });
                }
            }
            catch (Exception ex)
            {
                list.Add(new MenuHeader { id = Guid.NewGuid(), name = "DEBUG SQL ERROR: " + ex.Message });
            }
            return (list, total);
        }

        public Task CreateAsync(MenuHeader menu) => Task.CompletedTask;
        public Task UpdateAsync(MenuHeader menu) => Task.CompletedTask;
        public Task DeleteAsync(Guid id) => Task.CompletedTask;

        private static string EscapeSort(string field) => field switch
        {
            "name"      => "m.name",
            "menu_type" => "m.menu_type",
            "is_active" => "m.is_active",
            _           => "m.name"
        };
    }
}
