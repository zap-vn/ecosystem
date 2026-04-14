using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly EcosystemDbContext _context;
        public OrganizationRepository(EcosystemDbContext context) => _context = context;
        public Task<OrganizationUnit> GetByIdAsync(Guid id) => throw new NotImplementedException();
        public Task<IEnumerable<OrganizationUnit>> GetAllAsync() => throw new NotImplementedException();
        public Task AddAsync(OrganizationUnit entity) => throw new NotImplementedException();
        public Task UpdateAsync(OrganizationUnit entity) => throw new NotImplementedException();
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();
        public Task<(IEnumerable<OrganizationUnit> Items, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, string search, Guid? rootId) => throw new NotImplementedException();
    }
}
