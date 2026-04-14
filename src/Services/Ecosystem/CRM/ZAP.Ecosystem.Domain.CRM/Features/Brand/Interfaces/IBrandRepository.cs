using ZAP.Ecosystem.Domain.CRM.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Brand.Domain.Entities;

namespace CRM.Brand.Domain.Interfaces
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Domain.Entities.Brand>> GetAllAsync(Guid? tenantId = null);
        Task<Domain.Entities.Brand?> GetByIdAsync(Guid id);
        Task<(IEnumerable<Domain.Entities.Brand> Items, int Total)> GetPagedAsync(
            int page,
            int pageSize,
            Guid? tenantId = null,
            string? search = null,
            int? statusId = null,
            string sortField = "name",
            bool sortDescending = false);
        Task CreateAsync(Domain.Entities.Brand brand);
        Task UpdateAsync(Domain.Entities.Brand brand);
        Task DeleteAsync(Guid id);
    }
}

