using CRM.Unit.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Unit.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<CRM.Unit.Domain.Entities.Product>> GetAllAsync();
        Task<CRM.Unit.Domain.Entities.Product?> GetByIdAsync(string id);
        Task CreateAsync(CRM.Unit.Domain.Entities.Product product);
        Task UpdateAsync(CRM.Unit.Domain.Entities.Product product);
        Task DeleteAsync(string id);
        Task<(IEnumerable<CRM.Unit.Domain.Entities.ProductVariant> Items, int TotalCount)> GetPagedAsync(
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

        Task<(IEnumerable<CRM.Unit.Domain.Entities.Product> Items, int TotalCount)> GetPagedProductsAsync(
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

