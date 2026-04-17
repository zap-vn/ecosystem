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
using ZAP.CRM.Catalog.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.CRM.Domain;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM.Customers;
    public class CustomerGroupRepository : ZAP.Ecosystem.Shared.Data.BaseRepository<CustomerGroup>, ICustomerGroupRepository
    {
        private readonly EcosystemDbContext _context;
        public CustomerGroupRepository(EcosystemDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<(IEnumerable<CustomerGroup> Items, int TotalCount)> GetPagedAsync(int pageIndex, int pageSize, Guid? tenantId = null, string? search = null) => throw new NotImplementedException();
        public Task<CustomerGroup?> GetByCodeAsync(string code) => throw new NotImplementedException();
        public Task<CustomerGroupTranslation?> GetTranslationAsync(Guid id, int localeId) => throw new NotImplementedException();
        public Task UpsertTranslationAsync(CustomerGroupTranslation translation) => throw new NotImplementedException();
    }




