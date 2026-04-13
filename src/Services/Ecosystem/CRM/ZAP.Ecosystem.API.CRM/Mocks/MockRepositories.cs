using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Product.Domain.Entities;
using CRM.Product.Domain.Interfaces;
using ZAP.Ecosystem.Shared.Interfaces;

namespace ZAP.Ecosystem.API.CRM.Mocks
{
    public class MockUserService : ICurrentUserService
    {
        public string? UserId => Guid.NewGuid().ToString();
        public string? TenantId => Guid.NewGuid().ToString();
        public string? UserGuid => Guid.NewGuid().ToString();
        public int LocaleId => 2;
    }

    public class MockProductRepository : IProductRepository
    {
        public Task<IEnumerable<global::CRM.Product.Domain.Entities.Product>> GetAllAsync() => Task.FromResult(Enumerable.Empty<global::CRM.Product.Domain.Entities.Product>());
        public Task<global::CRM.Product.Domain.Entities.Product?> GetByIdAsync(string id) => Task.FromResult<global::CRM.Product.Domain.Entities.Product?>(new global::CRM.Product.Domain.Entities.Product { id = Guid.NewGuid() });
        public Task CreateAsync(global::CRM.Product.Domain.Entities.Product product) => Task.CompletedTask;
        public Task UpdateAsync(global::CRM.Product.Domain.Entities.Product product) => Task.CompletedTask;
        public Task DeleteAsync(string id) => Task.CompletedTask;
        
        public Task<(IEnumerable<ProductVariant> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, Guid? tenantId = null, string? searchTerm = null, int? statusId = null, Guid? categoryId = null, Guid? locationId = null, int localeId = 2, int? productTypeId = null, string sortField = "created_at", bool sortDescending = true) 
            => Task.FromResult<(IEnumerable<ProductVariant>, int)>((new List<ProductVariant>(), 0));

        public Task<(IEnumerable<global::CRM.Product.Domain.Entities.Product> Items, int TotalCount)> GetPagedProductsAsync(int page, int pageSize, Guid? tenantId = null, string? searchTerm = null, int? statusId = null, Guid? categoryId = null, Guid? locationId = null, int localeId = 2, int? productTypeId = null, string sortField = "created_at", bool sortDescending = true)
            => Task.FromResult<(IEnumerable<global::CRM.Product.Domain.Entities.Product>, int)>((new List<global::CRM.Product.Domain.Entities.Product>(), 0));
    }

    public class MockUnitRepository : IUnitRepository
    {
        public Task<IEnumerable<UomItem>> GetAllAsync(Guid? tenantId = null) => Task.FromResult(Enumerable.Empty<UomItem>());
        public Task<UomItem?> GetByIdAsync(int id) => Task.FromResult<UomItem?>(new UomItem { id = id });
        public Task CreateAsync(UomItem uomItem) => Task.CompletedTask;
        public Task UpdateAsync(UomItem uomItem) => Task.CompletedTask;
        public Task DeleteAsync(int id) => Task.CompletedTask;
        public Task<(IEnumerable<UomItem> Items, int Total)> GetPagedAsync(int page, int pageSize, Guid? tenantId = null, string? search = null, int? statusId = null, int? precision = null, string sortField = "name", bool sortDescending = false)
            => Task.FromResult<(IEnumerable<UomItem>, int)>((new List<UomItem>(), 0));
    }

    public class MockCategoryRepository : ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAllAsync() => Task.FromResult(Enumerable.Empty<Category>());
        public Task<Category?> GetByIdAsync(Guid id) => Task.FromResult<Category?>(new Category { id = id });
        public Task CreateAsync(Category category) => Task.CompletedTask;
        public Task UpdateAsync(Category category) => Task.CompletedTask;
        public Task DeleteAsync(Guid id) => Task.CompletedTask;
        public Task<(IEnumerable<Category> Items, int Total)> GetPagedAsync(int page, int pageSize, Guid? tenantId = null, string? search = null, int? statusId = null, string sortField = "name", bool sortDescending = false)
            => Task.FromResult<(IEnumerable<Category>, int)>((new List<Category>(), 0));
    }

    public class MockModifierGroupRepository : IModifierGroupRepository
    {
        public Task<IEnumerable<ModifierGroup>> GetAllAsync(Guid? tenantId = null) => Task.FromResult(Enumerable.Empty<ModifierGroup>());
        public Task<ModifierGroup?> GetByIdAsync(Guid id) => Task.FromResult<ModifierGroup?>(new ModifierGroup { id = id });
        public Task CreateAsync(ModifierGroup modifierGroup) => Task.CompletedTask;
        public Task UpdateAsync(ModifierGroup modifierGroup) => Task.CompletedTask;
        public Task DeleteAsync(Guid id) => Task.CompletedTask;
        public Task<(IEnumerable<ModifierGroup> Items, int Total)> GetPagedAsync(int page, int pageSize, Guid? tenantId = null, string? search = null, int? statusId = null, string? displayType = null, string sortField = "name", bool sortDescending = false)
            => Task.FromResult<(IEnumerable<ModifierGroup>, int)>((new List<ModifierGroup>(), 0));
    }

    public class MockBrandRepository : IBrandRepository
    {
        public Task<IEnumerable<Brand>> GetAllAsync(Guid? tenantId = null) => Task.FromResult(Enumerable.Empty<Brand>());
        public Task<Brand?> GetByIdAsync(Guid id) => Task.FromResult<Brand?>(new Brand { id = id });
        public Task CreateAsync(Brand brand) => Task.CompletedTask;
        public Task UpdateAsync(Brand brand) => Task.CompletedTask;
        public Task DeleteAsync(Guid id) => Task.CompletedTask;
        public Task<(IEnumerable<Brand> Items, int Total)> GetPagedAsync(int page, int pageSize, Guid? tenantId = null, string? search = null, int? statusId = null, string sortField = "name", bool sortDescending = false)
            => Task.FromResult<(IEnumerable<Brand>, int)>((new List<Brand>(), 0));
    }
}
