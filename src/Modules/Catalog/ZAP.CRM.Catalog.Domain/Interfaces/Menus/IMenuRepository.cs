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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZAP.CRM.Catalog.Domain.Interfaces.Menus;
    public interface IMenuRepository : ZAP.Ecosystem.Shared.Data.IBaseRepository<MenuHeader>
    {
        Task<(IEnumerable<MenuHeader> Items, int Total)> GetPagedAsync(
            int page,
            int pageSize,
            Guid? tenantId = null,
            string? search = null,
            bool? isActive = null,
            string? menuType = null,
            int localeId = 2,
            string sortField = "name",
            bool sortDescending = false);
    }



