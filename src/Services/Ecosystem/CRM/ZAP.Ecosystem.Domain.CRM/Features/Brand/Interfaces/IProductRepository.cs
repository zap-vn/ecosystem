using CRM.Brand.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Brand.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<CRM.Brand.Domain.Entities.Product>> GetAllAsync();
        Task<CRM.Brand.Domain.Entities.Product?> GetByIdAsync(string id);
        Task CreateAsync(CRM.Brand.Domain.Entities.Product product);
        Task UpdateAsync(CRM.Brand.Domain.Entities.Product product);
        Task DeleteAsync(string id);
        Task<(IEnumerable<CRM.Brand.Domain.Entities.ProductVariant> Items, int TotalCount)> GetPagedAsync(
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

        Task<(IEnumerable<CRM.Brand.Domain.Entities.Product> Items, int TotalCount)> GetPagedProductsAsync(
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

