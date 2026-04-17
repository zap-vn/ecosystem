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
using ZAP.Ecosystem.CRM.Domain;
using ZAP.Ecosystem.HRM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ZAP.Ecosystem.HRM.Infrastructure.Data.Repositories;
    public class EmployeeRepository : ZAP.Ecosystem.Shared.Data.BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly HRMDbContext _context;
        public EmployeeRepository(HRMDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Employee> Items, int TotalCount)> GetPagedAsync(
            int pageIndex, int pageSize, 
            Guid? tenantId = null, string? search = null, 
            Guid? organizationUnitId = null, int? statusId = null)
        {
            var query = _context.Employees
                .AsNoTracking()
                .AsQueryable();

            if (tenantId.HasValue)
                query = query.Where(e => e.tenant_id == tenantId);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(e => e.full_name.Contains(search) || 
                                       e.employee_code.Contains(search) ||
                                       e.email.Contains(search));
            }

            if (statusId.HasValue)
                query = query.Where(e => e.status_id == statusId);

            // Add sorting if needed, default by created_at or id
            query = query.OrderByDescending(e => e.created_at);

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<Employee?> GetByCodeAsync(string code)
        {
            return await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.employee_code == code);
        }

        public Task<EmployeeTranslation?> GetTranslationAsync(string code, string language) => Task.FromResult<EmployeeTranslation?>(null);
        public Task UpsertTranslationAsync(EmployeeTranslation translation) => Task.CompletedTask;
    }




