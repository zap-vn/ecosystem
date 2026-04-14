using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EcosystemDbContext _context;
        public EmployeeRepository(EcosystemDbContext context) => _context = context;
        public Task<Employee?> GetByIdAsync(Guid id) => throw new NotImplementedException();
        public Task<IEnumerable<Employee>> GetAllAsync() => throw new NotImplementedException();
        public Task AddAsync(Employee entity) => throw new NotImplementedException();
        public Task UpdateAsync(Employee entity) => throw new NotImplementedException();
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();
        public Task<(IEnumerable<Employee> Items, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, Guid? tenantId = null, string? search = null, Guid? organizationUnitId = null, int? statusId = null) => throw new NotImplementedException();
        public Task<Employee?> GetByCodeAsync(string code) => throw new NotImplementedException();
        public Task<EmployeeTranslation?> GetTranslationAsync(string code, string language) => throw new NotImplementedException();
        public Task UpsertTranslationAsync(EmployeeTranslation translation) => throw new NotImplementedException();
    }
}
