using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class CustomerGroupRepository : ICustomerGroupRepository
    {
        private readonly EcosystemDbContext _context;
        public CustomerGroupRepository(EcosystemDbContext context) => _context = context;
        public Task<CustomerGroup?> GetByIdAsync(Guid id) => throw new NotImplementedException();
        public Task<IEnumerable<CustomerGroup>> GetAllAsync() => throw new NotImplementedException();
        public Task AddAsync(CustomerGroup entity) => throw new NotImplementedException();
        public Task UpdateAsync(CustomerGroup entity) => throw new NotImplementedException();
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();
        public Task<(IEnumerable<CustomerGroup> Items, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, Guid? tenantId = null, string? search = null) => throw new NotImplementedException();
        public Task<CustomerGroup?> GetByCodeAsync(string code) => throw new NotImplementedException();
        public Task<CustomerGroupTranslation?> GetTranslationAsync(Guid id, int localeId) => throw new NotImplementedException();
        public Task UpsertTranslationAsync(CustomerGroupTranslation translation) => throw new NotImplementedException();
    }
}
