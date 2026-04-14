using ZAP.Ecosystem.Domain.CRM.Common;
using CRM.Management.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Management.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<CRM.Management.Domain.Entities.Product>> GetAllAsync();
        Task<CRM.Management.Domain.Entities.Product?> GetByIdAsync(string id);
        Task CreateAsync(CRM.Management.Domain.Entities.Product product);
        Task UpdateAsync(CRM.Management.Domain.Entities.Product product);
        Task DeleteAsync(string id);
        Task<(IEnumerable<CRM.Management.Domain.Entities.ProductVariant> Items, int TotalCount)> GetPagedAsync(
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

        Task<(IEnumerable<CRM.Management.Domain.Entities.Product> Items, int TotalCount)> GetPagedProductsAsync(
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


