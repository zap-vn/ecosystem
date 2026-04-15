using ZAP.Ecosystem.Domain.CRM.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Domain.CRM
{
    public interface ICustomerRepository : ZAP.Ecosystem.Shared.Data.IBaseRepository<CustomerEntity>
    {
        Task<ZAP.Ecosystem.Shared.Data.PagedResult<CustomerEntity>> GetPagedAsync(
            int pageIndex,
            int pageSize,
            Guid? tenantId = null,
            string? search = null,
            int? statusId = null,
            Guid? tierId = null,
            decimal? minTotalSpent = null,
            decimal? maxTotalSpent = null,
            decimal? minPoints = null,
            decimal? maxPoints = null,
            string sortField = "full_name",
            bool sortDescending = false);
        Task<CustomerEntity?> GetByIdAsync(string id);
        Task<CustomerEntity> CreateAsync(CustomerEntity entity);
        Task<bool> UpdateAsync(CustomerEntity entity);
        Task<bool> DeleteAsync(string id);
    }
}

