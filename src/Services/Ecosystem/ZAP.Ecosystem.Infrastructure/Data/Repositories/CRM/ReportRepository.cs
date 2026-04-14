using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class ReportRepository : IReportRepository
    {
        private readonly EcosystemDbContext _context;
        public ReportRepository(EcosystemDbContext context) => _context = context;
        public Task<ReportTemplate> GetByIdAsync(Guid id) => throw new NotImplementedException();
        public Task<IEnumerable<ReportTemplate>> GetAllAsync() => throw new NotImplementedException();
        public Task AddAsync(ReportTemplate entity) => throw new NotImplementedException();
        public Task UpdateAsync(ReportTemplate entity) => throw new NotImplementedException();
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();
    }
}
