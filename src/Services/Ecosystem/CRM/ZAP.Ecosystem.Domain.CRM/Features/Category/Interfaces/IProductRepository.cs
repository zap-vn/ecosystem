using CRM.Category.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Category.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<CRM.Category.Domain.Entities.Product>> GetAllAsync();
        Task<CRM.Category.Domain.Entities.Product?> GetByIdAsync(string id);
        Task CreateAsync(CRM.Category.Domain.Entities.Product product);
        Task UpdateAsync(CRM.Category.Domain.Entities.Product product);
        Task DeleteAsync(string id);
        Task<(IEnumerable<CRM.Category.Domain.Entities.ProductVariant> Items, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            Guid? tenantId = null,
            string? searchTerm = null,
            int? statusId = null,
            Guid? categoryId = null,
            Guid? locationId = null,
            int localeId = 2,
            int? productTypeId = null,
            string sortField = "created_at",
            bool sortDescending = true);

        Task<(IEnumerable<CRM.Category.Domain.Entities.Product> Items, int TotalCount)> GetPagedProductsAsync(
            int page,
            int pageSize,
            Guid? tenantId = null,
            string? searchTerm = null,
            int? statusId = null,
            Guid? categoryId = null,
            Guid? locationId = null,
            int localeId = 2,
            int? productTypeId = null,
            string sortField = "created_at",
            bool sortDescending = true);
    }
}

