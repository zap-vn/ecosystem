using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcosystemDbContext _context;
        public OrderRepository(EcosystemDbContext context) => _context = context;
        public Task<OrderEntity> GetByIdAsync(Guid id) => throw new NotImplementedException();
        public Task<IEnumerable<OrderEntity>> GetAllAsync() => throw new NotImplementedException();
        public Task AddAsync(OrderEntity entity) => throw new NotImplementedException();
        public Task UpdateAsync(OrderEntity entity) => throw new NotImplementedException();
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();
        public Task<IEnumerable<OrderEntity>> GetByStatusAsync(string status) => throw new NotImplementedException();
        public Task<Object> GetOrderSummaryAsync(string status, int page, int pageSize) => throw new NotImplementedException();
        public Task<(List<OrderEntity> Items, Int64 TotalCount)> GetOrderListAsync(string keyword, string status, int pageIndex, int pageSize) => throw new NotImplementedException();
        public Task<IReadOnlyList<OrderDetailEntity>> GetRelatedOrderDetailsAsync(IEnumerable<string> orderIds) => throw new NotImplementedException();
        public Task<IReadOnlyList<LocationEntity>> GetLocationsAsync(IEnumerable<string> locationGuids) => throw new NotImplementedException();
    }
}
