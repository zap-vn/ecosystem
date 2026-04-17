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
using ZAP.Ecosystem.Sales.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ZAP.Ecosystem.Sales.Infrastructure.Data.Repositories;
    public class OrderRepository : ZAP.Ecosystem.Shared.Data.BaseRepository<OrderEntity>, IOrderRepository
    {
        private readonly SalesDbContext _context;
        public OrderRepository(SalesDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderEntity>> GetByStatusAsync(string? status)
        {
            var query = _context.Set<OrderEntity>().AsNoTracking();
            if (int.TryParse(status, out var statusId))
                query = query.Where(o => o.status_id == statusId);
            return await query.ToListAsync();
        }

        public async Task<Object> GetOrderSummaryAsync(string? status, int page, int pageSize)
        {
            return new { TotalOrders = await _context.Set<OrderEntity>().CountAsync() };
        }

        public async Task<(List<OrderEntity> Items, Int64 TotalCount)> GetOrderListAsync(string? keyword, string? status, int pageIndex, int pageSize)
        {
            var query = _context.Set<OrderEntity>().AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(o => o.order_number.Contains(keyword));
            
            if (int.TryParse(status, out var statusId))
                query = query.Where(o => o.status_id == statusId);

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(o => o.created_at)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public Task<IReadOnlyList<OrderDetailEntity>> GetRelatedOrderDetailsAsync(IEnumerable<string> orderIds) => Task.FromResult<IReadOnlyList<OrderDetailEntity>>(new List<OrderDetailEntity>());
        public Task<IReadOnlyList<LocationEntity>> GetLocationsAsync(IEnumerable<string> locationGuids) => Task.FromResult<IReadOnlyList<LocationEntity>>(new List<LocationEntity>());
    }




