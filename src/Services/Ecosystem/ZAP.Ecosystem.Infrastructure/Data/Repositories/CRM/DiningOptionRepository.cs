using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class DiningOptionRepository : IDiningOptionRepository
    {
        private readonly EcosystemDbContext _context;
        public DiningOptionRepository(EcosystemDbContext context) => _context = context;

        public Task<IEnumerable<DiningOption>> GetAllAsync(int localeId) => throw new NotImplementedException();
        public Task<DiningOption> GetByIdAsync(int id, int? localeId) => throw new NotImplementedException();
        public Task CreateAsync(DiningOption entity) => throw new NotImplementedException();
        public Task UpdateAsync(DiningOption entity) => throw new NotImplementedException();
    }
}

