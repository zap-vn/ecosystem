using CRM.Category.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Category.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Domain.Entities.Category>> GetAllAsync(Guid? tenantId = null);
        Task<Domain.Entities.Category?> GetByIdAsync(Guid id);
        Task<(IEnumerable<Domain.Entities.Category> Items, int Total)> GetPagedAsync(
            int page,
            int pageSize,
            Guid? tenantId = null,
            string? search = null,
            int? statusId = null,
            Guid? parentId = null,
            string sortField = "name",
            bool sortDescending = false);
        Task CreateAsync(Domain.Entities.Category category);
        Task UpdateAsync(Domain.Entities.Category category);
        Task DeleteAsync(Guid id);
    }
}
