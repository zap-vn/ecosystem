using ZAP.CRM.Catalog.Domain.Interfaces.Brands;
using ZAP.CRM.Catalog.Domain.Interfaces.Products;
using ZAP.CRM.Catalog.Domain.Interfaces.Menus;
using ZAP.CRM.Catalog.Domain.Interfaces.Categories;
using ZAP.CRM.Catalog.Domain.Interfaces.Locations;
using ZAP.CRM.Catalog.Domain.Interfaces.Modifiers;
using ZAP.CRM.Catalog.Domain.Interfaces.Geography;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Menus;
using ZAP.CRM.Catalog.Domain.Entities.Categories;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using ZAP.CRM.Catalog.Domain.Entities.Modifiers;
using ZAP.CRM.Catalog.Domain.Entities.Geography;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using ZAP.CRM.Catalog.Domain.Interfaces;
using ZAP.Ecosystem.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZAP.CRM.Catalog.Domain.Interfaces.Products;
    public interface IProductRepository : ZAP.Ecosystem.Shared.Data.IBaseRepository<Product>
    {
        Task<(IEnumerable<ProductVariant> Items, int TotalCount)> GetPagedAsync(
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

        Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedProductsAsync(
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



