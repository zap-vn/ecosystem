using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.API.CRM
{
    public class MockModifierItemRepository : IModifierItemRepository
    {
        private readonly EcosystemDbContext _context;
        public MockModifierItemRepository(EcosystemDbContext context) { _context = context; }

        public Task<ModifierItem?> GetByIdAsync(Guid id) => Task.FromResult<ModifierItem?>(null);

        public async Task<IEnumerable<ModifierItem>> GetAllAsync(Guid? groupId = null)
        {
            var (items, _) = await GetPagedAsync(1, 1000, groupId);
            return items;
        }

        public async Task<(IEnumerable<ModifierItem> Items, int Total)> GetPagedAsync(
            int page, int pageSize,
            Guid? groupId = null,
            int? statusId = null,
            string sortField = "sort_order",
            bool sortDescending = false)
        {
            var list = new List<ModifierItem>();
            int total = 0;
            try
            {
                pageSize = pageSize > 0 ? pageSize : 10;
                page     = page     > 0 ? page     : 1;
                int offset = (page - 1) * pageSize;

                var conditions = new List<string>();
                if (groupId.HasValue)   conditions.Add($"group_id = '{groupId.Value}'");
                if (statusId.HasValue)  conditions.Add($"status_id = {statusId.Value}");
                string where = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";

                var conn = _context.Database.GetDbConnection();
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();

                // Count
                using (var countCmd = conn.CreateCommand())
                {
                    countCmd.CommandText = $"SELECT COUNT(*) FROM catalog.modifier_item {where}";
                    var countResult = await countCmd.ExecuteScalarAsync();
                    total = Convert.ToInt32(countResult);
                }

                // Fetch page
                using var cmd = conn.CreateCommand();
                cmd.CommandText = $@"
                    SELECT m.id, m.serial_id, m.serial_number, m.group_id, m.product_variant_id,
                           m.image_url, m.price_override, m.sort_order, m.status_id, m.created_at, m.updated_at,
                           si.code AS status_code, sit.name AS status_name
                    FROM catalog.modifier_item m
                    LEFT JOIN system.status_item si ON si.id = m.status_id
                    LEFT JOIN system.status_item_translation sit ON sit.status_item_id = si.id AND sit.locale_id = 2
                    {where}
                    ORDER BY {EscapeSort(sortField)} {(sortDescending ? "DESC" : "ASC")}
                    LIMIT {pageSize} OFFSET {offset}";

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    list.Add(new ModifierItem
                    {
                        id                 = reader.IsDBNull(0)  ? Guid.Empty : reader.GetGuid(0),
                        serial_id          = reader.IsDBNull(1)  ? null       : reader.GetInt32(1),
                        serial_number      = reader.IsDBNull(2)  ? null       : reader.GetString(2),
                        group_id           = reader.IsDBNull(3)  ? null       : reader.GetGuid(3),
                        product_variant_id = reader.IsDBNull(4)  ? null       : reader.GetGuid(4),
                        image_url          = reader.IsDBNull(5)  ? null       : reader.GetString(5),
                        price_override     = reader.IsDBNull(6)  ? null       : reader.GetDecimal(6),
                        sort_order         = reader.IsDBNull(7)  ? 0          : reader.GetInt32(7),
                        status_id          = reader.IsDBNull(8)  ? null       : reader.GetInt32(8),
                        created_at         = reader.IsDBNull(9)  ? DateTime.MinValue : reader.GetDateTime(9),
                        updated_at         = reader.IsDBNull(10) ? null       : reader.GetDateTime(10),
                        status_code        = reader.IsDBNull(11) ? null       : reader.GetString(11),
                        status_name        = reader.IsDBNull(12) ? null       : reader.GetString(12),
                    });
                }
            }
            catch (Exception ex)
            {
                list.Add(new ModifierItem { id = Guid.NewGuid(), sort_order = -1, image_url = "DEBUG SQL ERROR: " + ex.Message });
            }
            return (list, total);
        }

        private static string EscapeSort(string field) => field switch
        {
            "sort_order"  => "sort_order",
            "created_at"  => "created_at",
            "status_id"   => "status_id",
            _             => "sort_order"
        };
    }
}
