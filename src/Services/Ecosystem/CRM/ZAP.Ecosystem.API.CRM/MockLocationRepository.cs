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
        public Task<IEnumerable<Location>> GetPagedAsync(LocationListFilter filter) => Task.FromResult<IEnumerable<Location>>(new List<Location>());
        public Task<IEnumerable<GeoProvince>> GetProvincesAsync(int localeId) => Task.FromResult<IEnumerable<GeoProvince>>(new List<GeoProvince>());
        public Task<int> GetTotalCountAsync(LocationListFilter filter) => Task.FromResult(0);
        public Task UpdateAsync(Location location) => Task.CompletedTask;
    }
}
