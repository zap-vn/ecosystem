using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Domain.CRM.Common;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.API.CRM
{
    public class MockLocationRepository : ILocationRepository
    {
        private readonly EcosystemDbContext _context;
        public MockLocationRepository(EcosystemDbContext context) { _context = context; }

        public Task CreateAsync(Location location) => Task.CompletedTask;
        public Task CreateStoreAsync(Store store) => Task.CompletedTask;
        public Task DeleteAsync(Guid id) => Task.CompletedTask;
        public Task<Location?> GetByIdAsync(Guid id) => Task.FromResult<Location?>(null);
        public Task<IEnumerable<GeoProvince>> GetProvincesAsync(int localeId) => Task.FromResult<IEnumerable<GeoProvince>>(new List<GeoProvince>());
        public Task UpdateAsync(Location location) => Task.CompletedTask;

        public async Task<IEnumerable<Location>> GetPagedAsync(LocationListFilter filter)
        {
            var list = new List<Location>();
            try
            {
                var conn = _context.Database.GetDbConnection();
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();

                int pageSize  = filter.PageSize  > 0 ? filter.PageSize  : 10;
                int pageIndex = filter.PageIndex > 0 ? filter.PageIndex : 1;
                int offset    = (pageIndex - 1) * pageSize;

                using var cmd = conn.CreateCommand();
                cmd.CommandText = $@"
                    SELECT id, serial_id, serial_number, location_code, tenant_id,
                           node_id, legacy_id, business_name, name, slug,
                           description, location_type_id, address_line_1, address_line_2, city,
                           state, country_id, province_id, district_id, ward_id,
                           zipcode, phone_number, email, website, twitter,
                           instagram, facebook, logo_url, cover_image_url, brand_color,
                           timezone, latitude, longitude, operating_hours, transfer_account,
                           transfer_tag, parent_location_id, status_id, is_active, created_at,
                           updated_at
                    FROM commerce.location
                    ORDER BY name
                    LIMIT {pageSize} OFFSET {offset}";

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    list.Add(new Location
                    {
                        id                 = reader.IsDBNull(0)  ? Guid.Empty       : reader.GetGuid(0),
                        serial_id          = reader.IsDBNull(1)  ? 0                : reader.GetInt64(1),
                        serial_number      = reader.IsDBNull(2)  ? string.Empty     : reader.GetString(2),
                        location_code      = reader.IsDBNull(3)  ? string.Empty     : reader.GetString(3),
                        tenant_id          = reader.IsDBNull(4)  ? null             : reader.GetGuid(4),
                        node_id            = reader.IsDBNull(5)  ? null             : reader.GetGuid(5),
                        legacy_id          = reader.IsDBNull(6)  ? null             : reader.GetString(6),
                        business_name      = reader.IsDBNull(7)  ? string.Empty     : reader.GetString(7),
                        name               = reader.IsDBNull(8)  ? string.Empty     : reader.GetString(8),
                        slug               = reader.IsDBNull(9)  ? string.Empty     : reader.GetString(9),
                        description        = reader.IsDBNull(10) ? null             : reader.GetString(10),
                        location_type_id   = reader.IsDBNull(11) ? null             : reader.GetInt32(11),
                        address_line_1     = reader.IsDBNull(12) ? null             : reader.GetString(12),
                        address_line_2     = reader.IsDBNull(13) ? null             : reader.GetString(13),
                        city               = reader.IsDBNull(14) ? null             : reader.GetString(14),
                        state              = reader.IsDBNull(15) ? null             : reader.GetString(15),
                        country_id         = reader.IsDBNull(16) ? null             : reader.GetInt32(16),
                        province_id        = reader.IsDBNull(17) ? null             : reader.GetInt32(17),
                        district_id        = reader.IsDBNull(18) ? null             : reader.GetInt32(18),
                        ward_id            = reader.IsDBNull(19) ? null             : reader.GetInt32(19),
                        zipcode            = reader.IsDBNull(20) ? null             : reader.GetString(20),
                        phone_number       = reader.IsDBNull(21) ? null             : reader.GetString(21),
                        email              = reader.IsDBNull(22) ? null             : reader.GetString(22),
                        website            = reader.IsDBNull(23) ? null             : reader.GetString(23),
                        twitter            = reader.IsDBNull(24) ? null             : reader.GetString(24),
                        instagram          = reader.IsDBNull(25) ? null             : reader.GetString(25),
                        facebook           = reader.IsDBNull(26) ? null             : reader.GetString(26),
                        logo_url           = reader.IsDBNull(27) ? null             : reader.GetString(27),
                        cover_image_url    = reader.IsDBNull(28) ? null             : reader.GetString(28),
                        brand_color        = reader.IsDBNull(29) ? null             : reader.GetString(29),
                        timezone           = reader.IsDBNull(30) ? null             : reader.GetString(30),
                        latitude           = reader.IsDBNull(31) ? null             : reader.GetDecimal(31),
                        longitude          = reader.IsDBNull(32) ? null             : reader.GetDecimal(32),
                        operating_hours    = null, // JSON — skip for now
                        transfer_account   = reader.IsDBNull(34) ? null             : reader.GetString(34),
                        transfer_tag       = reader.IsDBNull(35) ? null             : reader.GetString(35),
                        parent_location_id = reader.IsDBNull(36) ? null             : reader.GetGuid(36),
                        status_id          = reader.IsDBNull(37) ? 0                : reader.GetInt32(37),
                        is_active          = !reader.IsDBNull(38) && reader.GetBoolean(38),
                        created_at         = reader.IsDBNull(39) ? DateTime.UtcNow  : reader.GetDateTime(39),
                        updated_at         = reader.IsDBNull(40) ? DateTime.UtcNow  : reader.GetDateTime(40),
                    });
                }
            }
            catch (Exception ex)
            {
                list.Add(new Location { id = Guid.NewGuid(), name = "DEBUG SQL ERROR: " + ex.Message });
            }
            return list;
        }

        public async Task<int> GetTotalCountAsync(LocationListFilter filter)
        {
            try
            {
                var conn = _context.Database.GetDbConnection();
                if (conn.State != System.Data.ConnectionState.Open)
                    await conn.OpenAsync();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM commerce.location";
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return reader.GetInt32(0);
                }
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
                return 9999;
            }
            return 0;
        }
    }
}
