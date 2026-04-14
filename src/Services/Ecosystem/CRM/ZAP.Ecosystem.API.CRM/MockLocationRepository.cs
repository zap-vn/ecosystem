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

                using var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT id, name, location_code, address_line_1, status_id, created_at, phone_number, email FROM public.locations LIMIT @PageSize OFFSET @Offset";
                
                var paramLimit = cmd.CreateParameter();
                paramLimit.ParameterName = "@PageSize";
                paramLimit.Value = filter.PageSize > 0 ? filter.PageSize : 10;
                cmd.Parameters.Add(paramLimit);

                var paramOffset = cmd.CreateParameter();
                paramOffset.ParameterName = "@Offset";
                paramOffset.Value = (filter.PageIndex > 0 ? filter.PageIndex - 1 : 0) * (filter.PageSize > 0 ? filter.PageSize : 10);
                cmd.Parameters.Add(paramOffset);

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    list.Add(new Location
                    {
                        id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                        name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        location_code = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        address_line_1 = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        status_id = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                        created_at = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                        phone_number = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                        email = reader.IsDBNull(7) ? string.Empty : reader.GetString(7)
                    });
                }
            }
            catch (Exception)
            {
                // Fallback to empty context if table fails
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
                cmd.CommandText = "SELECT COUNT(*) FROM public.locations";
                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return reader.GetInt32(0);
                }
            }
            catch (Exception) { }
            return 0;
        }
    }
}
