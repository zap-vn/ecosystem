using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.API.CRM
{
    public class MockModifierGroupRepository : IModifierGroupRepository
    {
        private readonly EcosystemDbContext _context;
        public MockModifierGroupRepository(EcosystemDbContext context) { _context = context; }

        public Task CreateAsync(ModifierGroup modifierGroup) => Task.CompletedTask;
        public Task UpdateAsync(ModifierGroup modifierGroup) => Task.CompletedTask;
        public Task DeleteAsync(Guid id) => Task.CompletedTask;
        public Task<ModifierGroup?> GetByIdAsync(Guid id) => Task.FromResult<ModifierGroup?>(null);

        public async Task<IEnumerable<ModifierGroup>> GetAllAsync(Guid? tenantId = null)
        {
            var (items, _) = await GetPagedAsync(1, 1000, tenantId);
            return items;
        }

        public async Task<(IEnumerable<ModifierGroup> Items, int Total)> GetPagedAsync(
            int page, int pageSize,
            Guid? tenantId = null, string? search = null,
            int? statusId = null, string? displayType = null,
            string sortField = "name", bool sortDescending = false)
        {
            var list = new List<ModifierGroup>();
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
                    countCmd.CommandText = "SELECT COUNT(*) FROM catalog.modifier_group";
                    var countResult = await countCmd.ExecuteScalarAsync();
                    total = Convert.ToInt32(countResult);
                }

                // Fetch page
                using var cmd = conn.CreateCommand();
                cmd.CommandText = $@"
                    SELECT mg.id, mg.tenant_id, mg.serial_id, mg.serial_number, mg.legacy_id,
                           mg.name, mg.description, mg.image_url, mg.min_selection, mg.max_selection,
                           mg.is_required, mg.sort_order, mg.status_id,
                           si.code AS status_code, sit.name AS status_name
                    FROM catalog.modifier_group mg
                    LEFT JOIN system.status_item si ON si.id = mg.status_id
                    LEFT JOIN system.status_item_translation sit ON sit.status_item_id = si.id AND sit.locale_id = 2
                    ORDER BY mg.{EscapeSort(sortField)} {(sortDescending ? "DESC" : "ASC")}
                    LIMIT {pageSize} OFFSET {offset}";

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    list.Add(new ModifierGroup
                    {
                        id            = reader.IsDBNull(0)  ? Guid.Empty    : reader.GetGuid(0),
                        tenant_id     = reader.IsDBNull(1)  ? Guid.Empty    : reader.GetGuid(1),
                        serial_id     = reader.IsDBNull(2)  ? null          : reader.GetInt32(2),
                        serial_number = reader.IsDBNull(3)  ? null          : reader.GetString(3),
                        legacy_id     = reader.IsDBNull(4)  ? null          : reader.GetString(4),
                        name          = reader.IsDBNull(5)  ? string.Empty  : reader.GetString(5),
                        description   = reader.IsDBNull(6)  ? null          : reader.GetString(6),
                        image_url     = reader.IsDBNull(7)  ? null          : reader.GetString(7),
                        min_selection = reader.IsDBNull(8)  ? 0             : reader.GetInt32(8),
                        max_selection = reader.IsDBNull(9)  ? 1             : reader.GetInt32(9),
                        is_required   = !reader.IsDBNull(10) && reader.GetBoolean(10),
                        sort_order    = reader.IsDBNull(11) ? 0             : reader.GetInt32(11),
                        status_id     = reader.IsDBNull(12) ? null          : reader.GetInt32(12),
                        status_code   = reader.IsDBNull(13) ? null          : reader.GetString(13),
                        status_name   = reader.IsDBNull(14) ? null          : reader.GetString(14),
                    });
                }
            }
            catch (Exception ex)
            {
                list.Add(new ModifierGroup { id = Guid.NewGuid(), name = "DEBUG SQL ERROR: " + ex.Message });
            }
            return (list, total);
        }

        private static string EscapeSort(string field) => field switch
        {
            "name"      => "name",
            "sort_order"=> "sort_order",
            "status_id" => "status_id",
            _           => "name"
        };
    }
}
