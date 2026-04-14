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
                    countCmd.CommandText = "SELECT COUNT(*) FROM people.customer";
                    var countResult = await countCmd.ExecuteScalarAsync();
                    total = Convert.ToInt32(countResult);
                }

                using var cmd = conn.CreateCommand();
                cmd.CommandText = $@"
                    SELECT c.id, c.serial_id, c.serial_number, c.tenant_id, c.customer_code, c.legacy_id, c.square_customer_id, c.reference_id, c.phone_number, c.email, c.full_name, c.first_name, c.last_name,
                           c.nickname, c.company_name, c.avatar_url, c.gender_id, c.birth_date, c.address_line_1, c.address_line_2, c.city_name, c.state_name, c.country_id, c.province_id, c.district_id,
                           c.ward_id, c.zipcode, c.preferred_locale_id, c.user_id, c.tier_id, c.memo, c.creation_source, c.email_subscription_status, c.is_instant_profile, c.current_points_balance, c.total_spent_amount, c.average_spent_amount, c.total_visits_count, c.first_visit_at, c.last_visit_at, c.status_id, c.display_initial, c.created_at, c.updated_at, c.group_id,
                           si.code AS status_code, sit.name AS status_name
                    FROM people.customer c
                    LEFT JOIN system.status_item si ON si.id = c.status_id
                    LEFT JOIN system.status_item_translation sit ON sit.status_item_id = si.id AND sit.locale_id = 2
                    ORDER BY {EscapeSort(sortField)} {(sortDescending ? "DESC" : "ASC")}
                    LIMIT {pageSize} OFFSET {offset}";

                using var reader = await cmd.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                {
                    var entity = new CustomerEntity
                    {
                        id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                        serial_id = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        serial_number = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        tenant_id = reader.IsDBNull(3) ? null : reader.GetGuid(3),
                        customer_code = reader.IsDBNull(4) ? "" : reader.GetString(4),
                        legacy_id = reader.IsDBNull(5) ? null : reader.GetString(5),
                        phone_number = reader.IsDBNull(8) ? null : reader.GetString(8),
                        email = reader.IsDBNull(9) ? null : reader.GetString(9),
                        full_name = reader.IsDBNull(10) ? null : reader.GetString(10),
                        first_name = reader.IsDBNull(11) ? null : reader.GetString(11),
                        last_name = reader.IsDBNull(12) ? null : reader.GetString(12),
                        nickname = reader.IsDBNull(13) ? null : reader.GetString(13),
                        company_name = reader.IsDBNull(14) ? null : reader.GetString(14),
                        avatar_url = reader.IsDBNull(15) ? null : reader.GetString(15),
                        gender_id = reader.IsDBNull(16) ? null : reader.GetInt32(16),
                        birth_date = reader.IsDBNull(17) ? null : reader.GetDateTime(17),
                        address_line_1 = reader.IsDBNull(18) ? null : reader.GetString(18),
                        address_line_2 = reader.IsDBNull(19) ? null : reader.GetString(19),
                        city_name = reader.IsDBNull(20) ? null : reader.GetString(20),
                        state_name = reader.IsDBNull(21) ? null : reader.GetString(21),
                        country_id = reader.IsDBNull(22) ? null : reader.GetInt32(22),
                        province_id = reader.IsDBNull(23) ? null : reader.GetInt32(23),
                        district_id = reader.IsDBNull(24) ? null : reader.GetInt32(24),
                        ward_id = reader.IsDBNull(25) ? null : reader.GetInt32(25),
                        zipcode = reader.IsDBNull(26) ? null : reader.GetString(26),
                        preferred_locale_id = reader.IsDBNull(27) ? null : reader.GetInt32(27),
                        user_id = reader.IsDBNull(28) ? null : reader.GetGuid(28),
                        tier_id = reader.IsDBNull(29) ? null : reader.GetGuid(29),
                        memo = reader.IsDBNull(30) ? null : reader.GetString(30),
                        creation_source = reader.IsDBNull(31) ? "Email" : reader.GetString(31),
                        email_subscription_status = reader.IsDBNull(32) ? null : reader.GetString(32),
                        is_instant_profile = !reader.IsDBNull(33) && reader.GetBoolean(33),
                        current_points_balance = reader.IsDBNull(34) ? 0 : reader.GetDecimal(34),
                        total_spent_amount = reader.IsDBNull(35) ? 0 : reader.GetDecimal(35),
                        status_id = reader.IsDBNull(40) ? null : reader.GetInt32(40),
                        display_initial = reader.IsDBNull(41) ? null : reader.GetString(41),
                        created_at = reader.IsDBNull(42) ? DateTime.UtcNow : reader.GetDateTime(42),
                        updated_at = reader.IsDBNull(43) ? null : reader.GetDateTime(43),
                        group_id = reader.IsDBNull(44) ? null : reader.GetGuid(44),
                        status_code = reader.IsDBNull(45) ? null : reader.GetString(45),
                        status_name = reader.IsDBNull(46) ? null : reader.GetString(46)
                    };

                    list.Add(entity);
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
