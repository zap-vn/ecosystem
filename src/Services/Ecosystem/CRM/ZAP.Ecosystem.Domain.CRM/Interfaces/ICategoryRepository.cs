using ZAP.Ecosystem.Domain.CRM.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Domain.CRM
{
    public interface ICategoryRepository : ZAP.Ecosystem.Shared.Data.IBaseRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllAsync(Guid? tenantId = null);
        Task<Category?> GetByIdAsync(Guid id);
        Task<(IEnumerable<Category> Items, int Total)> GetPagedAsync(
            int page,
            int pageSize,
            Guid? tenantId = null,
            string? search = null,
            int? statusId = null,
            Guid? parentId = null,
            string sortField = "name",
            bool sortDescending = false);
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Guid id);
    }
}

