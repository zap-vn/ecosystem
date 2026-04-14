using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;
using ZAP.Ecosystem.Shared.Data;

namespace ZAP.Ecosystem.API.CRM
{
    public class MockCustomerRepository : ICustomerRepository
    {
        private readonly EcosystemDbContext _context;
        public MockCustomerRepository(EcosystemDbContext context) { _context = context; }

        public Task<CustomerEntity> CreateAsync(CustomerEntity entity) => Task.FromResult(entity);
        public Task<bool> UpdateAsync(CustomerEntity entity) => Task.FromResult(true);
        public Task<bool> DeleteAsync(string id) => Task.FromResult(true);
        public Task<CustomerEntity?> GetByIdAsync(string id) => Task.FromResult<CustomerEntity?>(null);

        public async Task<PagedResult<CustomerEntity>> GetPagedAsync(
            int pageIndex, int pageSize,
            Guid? tenantId = null, string? search = null,
            int? statusId = null, Guid? tierId = null,
            decimal? minTotalSpent = null, decimal? maxTotalSpent = null,
            decimal? minPoints = null, decimal? maxPoints = null,
            string sortField = "full_name", bool sortDescending = false)
        {
            var list = new List<CustomerEntity>();
            int total = 0;
            try
            {
                pageSize  = pageSize  > 0 ? pageSize  : 10;
                pageIndex = pageIndex > 0 ? pageIndex : 1;
                int offset = (pageIndex - 1) * pageSize;

                var conn = _context.Database.GetDbConnection();
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();

                // Count
                using (var countCmd = conn.CreateCommand())
                {
                    countCmd.CommandText = "SELECT COUNT(*) FROM identity.customer";
                    var countResult = await countCmd.ExecuteScalarAsync();
                    total = Convert.ToInt32(countResult);
                }

                // Fetch page
                using var cmd = conn.CreateCommand();
                cmd.CommandText = $@"
                    SELECT id, tenant_id, legacy_id, email, phone_number,
                           full_name, gender, birth_date, country_id, province_id,
                           district_id, ward_id, zipcode, preferred_locale_id, user_id,
                           status_id, tier_id, group_id, current_points_balance, total_spent_amount,
                           created_at, updated_at
                    FROM identity.customer
                    ORDER BY {EscapeSort(sortField)} {(sortDescending ? "DESC" : "ASC")}
                    LIMIT {pageSize} OFFSET {offset}";

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    list.Add(new CustomerEntity
                    {
                        id                      = reader.IsDBNull(0)  ? Guid.Empty        : reader.GetGuid(0),
                        tenant_id               = reader.IsDBNull(1)  ? null              : reader.GetGuid(1),
                        legacy_id               = reader.IsDBNull(2)  ? null              : reader.GetString(2),
                        email                   = reader.IsDBNull(3)  ? null              : reader.GetString(3),
                        phone_number            = reader.IsDBNull(4)  ? null              : reader.GetString(4),
                        full_name               = reader.IsDBNull(5)  ? null              : reader.GetString(5),
                        gender                  = reader.IsDBNull(6)  ? null              : reader.GetString(6),
                        birth_date              = reader.IsDBNull(7)  ? null              : reader.GetDateTime(7),
                        country_id              = reader.IsDBNull(8)  ? null              : reader.GetInt32(8),
                        province_id             = reader.IsDBNull(9)  ? null              : reader.GetInt32(9),
                        district_id             = reader.IsDBNull(10) ? null              : reader.GetInt32(10),
                        ward_id                 = reader.IsDBNull(11) ? null              : reader.GetInt32(11),
                        zipcode                 = reader.IsDBNull(12) ? null              : reader.GetString(12),
                        preferred_locale_id     = reader.IsDBNull(13) ? null              : reader.GetInt32(13),
                        user_id                 = reader.IsDBNull(14) ? null              : reader.GetGuid(14),
                        status_id               = reader.IsDBNull(15) ? null              : reader.GetInt32(15),
                        tier_id                 = reader.IsDBNull(16) ? null              : reader.GetGuid(16),
                        group_id                = reader.IsDBNull(17) ? null              : reader.GetGuid(17),
                        current_points_balance  = reader.IsDBNull(18) ? null              : reader.GetDecimal(18),
                        total_spent_amount      = reader.IsDBNull(19) ? null              : reader.GetDecimal(19),
                    });
                }
            }
            catch (Exception ex)
            {
                list.Add(new CustomerEntity { id = Guid.NewGuid(), full_name = "DEBUG SQL ERROR: " + ex.Message });
            }
            return new PagedResult<CustomerEntity>(list, total, pageIndex, pageSize);
        }

        private static string EscapeSort(string field) => field switch
        {
            "full_name"          => "full_name",
            "email"              => "email",
            "phone_number"       => "phone_number",
            "total_spent_amount" => "total_spent_amount",
            "current_points_balance" => "current_points_balance",
            _                    => "full_name"
        };
    }
}
