using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;
using ZAP.Ecosystem.Shared.Data;

namespace ZAP.Ecosystem.API.CRM
{
    public class MockPromotionRepository : IPromotionRepository
    {
        private readonly EcosystemDbContext _context;
        public MockPromotionRepository(EcosystemDbContext context) { _context = context; }

        public Task AddAsync(PromotionEntity entity) => Task.CompletedTask;
        public Task UpdateAsync(PromotionEntity entity) => Task.CompletedTask;
        public Task DeleteAsync(Guid id) => Task.CompletedTask;
        public Task<PromotionEntity?> GetByIdAsync(Guid id) => Task.FromResult<PromotionEntity?>(null);
        public Task<IEnumerable<PromotionEntity>> GetAllAsync() => Task.FromResult<IEnumerable<PromotionEntity>>(new List<PromotionEntity>());

        public async Task<PagedResult<PromotionEntity>> GetPagedAsync(
            int pageIndex, int pageSize,
            Guid? tenantId = null, string? search = null,
            int? statusId = null, int? discountTypeId = null,
            string sortField = "name", bool sortDescending = false)
        {
            var list = new List<PromotionEntity>();
            int total = 0;
            try
            {
                pageSize  = pageSize  > 0 ? pageSize  : 10;
                pageIndex = pageIndex > 0 ? pageIndex : 1;
                int offset = (pageIndex - 1) * pageSize;

                var conn = _context.Database.GetDbConnection();
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();

                var clauses = new List<string>();
                if (!string.IsNullOrWhiteSpace(search))
                    clauses.Add($"p.name ILIKE '%{search.Replace("'", "''")}%'");
                if (statusId.HasValue)
                    clauses.Add($"p.status_id = {statusId.Value}");
                if (discountTypeId.HasValue)
                    clauses.Add($"p.discount_type_id = {discountTypeId.Value}");
                
                var where = clauses.Count > 0 ? "WHERE " + string.Join(" AND ", clauses) : "";

                using (var countCmd = conn.CreateCommand())
                {
                    countCmd.CommandText = $"SELECT COUNT(*) FROM marketing.promotion p {where}";
                    total = Convert.ToInt32(await countCmd.ExecuteScalarAsync());
                }

                using var cmd = conn.CreateCommand();
                cmd.CommandText = $@"
                    SELECT p.id, p.tenant_id, p.legacy_id, p.name, p.short_name, p.description,
                           p.status_id, p.discount_value, p.is_automatic,
                           si.code AS status_code, sit.name AS status_name
                    FROM marketing.promotion p
                    LEFT JOIN system.status_item si ON si.id = p.status_id
                    LEFT JOIN system.status_item_translation sit ON sit.status_item_id = si.id AND sit.locale_id = 2
                    {where}
                    ORDER BY p.{sortField} {(sortDescending ? "DESC" : "ASC")}
                    LIMIT {pageSize} OFFSET {offset}";

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    list.Add(new PromotionEntity
                    {
                        id             = reader.IsDBNull(0) ? Guid.Empty   : reader.GetGuid(0),
                        tenant_id      = reader.IsDBNull(1) ? null         : reader.GetGuid(1),
                        legacy_id      = reader.IsDBNull(2) ? null         : reader.GetString(2),
                        name           = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        short_name     = reader.IsDBNull(4) ? null         : reader.GetString(4),
                        description    = reader.IsDBNull(5) ? null         : reader.GetString(5),
                        status_id      = reader.IsDBNull(6) ? 0            : reader.GetInt32(6),
                        discount_value = reader.IsDBNull(7) ? 0            : reader.GetDecimal(7),
                        is_automatic   = !reader.IsDBNull(8) && reader.GetBoolean(8),
                        status_code    = reader.IsDBNull(9) ? null         : reader.GetString(9),
                        status_name    = reader.IsDBNull(10) ? null        : reader.GetString(10)
                    });
                }
            }
            catch (Exception ex)
            {
                list.Add(new PromotionEntity { id = Guid.NewGuid(), name = "DEBUG SQL ERROR: " + ex.Message });
            }
            return new PagedResult<PromotionEntity>(list, total, pageIndex, pageSize);
        }
    }
}
