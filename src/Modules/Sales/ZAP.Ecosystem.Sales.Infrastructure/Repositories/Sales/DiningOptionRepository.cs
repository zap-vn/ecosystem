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
using ZAP.Ecosystem.Sales.Infrastructure.Data;

namespace ZAP.Ecosystem.Sales.Infrastructure.Data.Repositories;
    public class DiningOptionRepository : ZAP.Ecosystem.Shared.Data.BaseRepository<DiningOption>, IDiningOptionRepository
    {
        private readonly EcosystemDbContext _context;
        public DiningOptionRepository(EcosystemDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<IEnumerable<DiningOption>> GetAllAsync(int localeId) => throw new NotImplementedException();
        public Task<DiningOption?> GetByIdAsync(int id, int? localeId) => throw new NotImplementedException();
    }




