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
                
                bool HasColumn(System.Data.IDataRecord r, string columnName)
                {
                    for (int i = 0; i < r.FieldCount; i++)
                    {
                        if (r.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                            return true;
                    }
                    return false;
                }

                while (await reader.ReadAsync())
                {
                    var entity = new CustomerEntity();
                    if (HasColumn(reader, "id") && !reader.IsDBNull(reader.GetOrdinal("id"))) entity.id = reader.GetGuid(reader.GetOrdinal("id"));
                    if (HasColumn(reader, "tenant_id") && !reader.IsDBNull(reader.GetOrdinal("tenant_id"))) entity.tenant_id = reader.GetGuid(reader.GetOrdinal("tenant_id"));
                    if (HasColumn(reader, "legacy_id") && !reader.IsDBNull(reader.GetOrdinal("legacy_id"))) entity.legacy_id = reader.GetString(reader.GetOrdinal("legacy_id"));
                    if (HasColumn(reader, "email") && !reader.IsDBNull(reader.GetOrdinal("email"))) entity.email = reader.GetString(reader.GetOrdinal("email"));
                    if (HasColumn(reader, "phone_number") && !reader.IsDBNull(reader.GetOrdinal("phone_number"))) entity.phone_number = reader.GetString(reader.GetOrdinal("phone_number"));
                    if (HasColumn(reader, "full_name") && !reader.IsDBNull(reader.GetOrdinal("full_name"))) entity.full_name = reader.GetString(reader.GetOrdinal("full_name"));
                    if (HasColumn(reader, "gender") && !reader.IsDBNull(reader.GetOrdinal("gender"))) entity.gender = reader.GetString(reader.GetOrdinal("gender"));
                    if (HasColumn(reader, "birth_date") && !reader.IsDBNull(reader.GetOrdinal("birth_date"))) entity.birth_date = reader.GetDateTime(reader.GetOrdinal("birth_date"));
                    if (HasColumn(reader, "country_id") && !reader.IsDBNull(reader.GetOrdinal("country_id"))) entity.country_id = reader.GetInt32(reader.GetOrdinal("country_id"));
                    if (HasColumn(reader, "province_id") && !reader.IsDBNull(reader.GetOrdinal("province_id"))) entity.province_id = reader.GetInt32(reader.GetOrdinal("province_id"));
                    if (HasColumn(reader, "district_id") && !reader.IsDBNull(reader.GetOrdinal("district_id"))) entity.district_id = reader.GetInt32(reader.GetOrdinal("district_id"));
                    if (HasColumn(reader, "ward_id") && !reader.IsDBNull(reader.GetOrdinal("ward_id"))) entity.ward_id = reader.GetInt32(reader.GetOrdinal("ward_id"));
                    if (HasColumn(reader, "zipcode") && !reader.IsDBNull(reader.GetOrdinal("zipcode"))) entity.zipcode = reader.GetString(reader.GetOrdinal("zipcode"));
                    if (HasColumn(reader, "preferred_locale_id") && !reader.IsDBNull(reader.GetOrdinal("preferred_locale_id"))) entity.preferred_locale_id = reader.GetInt32(reader.GetOrdinal("preferred_locale_id"));
                    if (HasColumn(reader, "user_id") && !reader.IsDBNull(reader.GetOrdinal("user_id"))) entity.user_id = reader.GetGuid(reader.GetOrdinal("user_id"));
                    if (HasColumn(reader, "status_id") && !reader.IsDBNull(reader.GetOrdinal("status_id"))) entity.status_id = reader.GetInt32(reader.GetOrdinal("status_id"));
                    if (HasColumn(reader, "status_code") && !reader.IsDBNull(reader.GetOrdinal("status_code"))) entity.status_code = reader.GetString(reader.GetOrdinal("status_code"));
                    if (HasColumn(reader, "status_name") && !reader.IsDBNull(reader.GetOrdinal("status_name"))) entity.status_name = reader.GetString(reader.GetOrdinal("status_name"));
                    if (HasColumn(reader, "tier_id") && !reader.IsDBNull(reader.GetOrdinal("tier_id"))) entity.tier_id = reader.GetGuid(reader.GetOrdinal("tier_id"));
                    if (HasColumn(reader, "group_id") && !reader.IsDBNull(reader.GetOrdinal("group_id"))) entity.group_id = reader.GetGuid(reader.GetOrdinal("group_id"));
                    if (HasColumn(reader, "current_points_balance") && !reader.IsDBNull(reader.GetOrdinal("current_points_balance"))) entity.current_points_balance = reader.GetDecimal(reader.GetOrdinal("current_points_balance"));
                    if (HasColumn(reader, "total_spent_amount") && !reader.IsDBNull(reader.GetOrdinal("total_spent_amount"))) entity.total_spent_amount = reader.GetDecimal(reader.GetOrdinal("total_spent_amount"));

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
