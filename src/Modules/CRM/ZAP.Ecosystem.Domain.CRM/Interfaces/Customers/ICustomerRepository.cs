using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using ZAP.Ecosystem.CRM.Domain.Entities.Customers;





using ZAP.Ecosystem.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.CRM.Domain.Interfaces.Customers;
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
    }
