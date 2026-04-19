using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;
using ZAP.Ecosystem.Shared.Data;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class CustomerRepository : ZAP.Ecosystem.Shared.Data.BaseRepository<CustomerEntity>, ICustomerRepository
    {
        public CustomerRepository(EcosystemDbContext context) : base(context) { }
        public Task<PagedResult<CustomerEntity>> GetPagedAsync(int pageIndex, int pageSize, Guid? tenantId = null, string? search = null, int? statusId = null, Guid? tierId = null, decimal? minTotalSpent = null, decimal? maxTotalSpent = null, decimal? minPoints = null, decimal? maxPoints = null, string sortField = "full_name", bool sortDescending = false) => throw new NotImplementedException();
        public Task<CustomerEntity?> GetByIdAsync(string id) => throw new NotImplementedException();
        public Task<CustomerEntity> CreateAsync(CustomerEntity entity) => throw new NotImplementedException();
        public Task<bool> UpdateAsync(CustomerEntity entity) => throw new NotImplementedException();
        public Task<bool> DeleteAsync(string id) => throw new NotImplementedException();
    }
}
