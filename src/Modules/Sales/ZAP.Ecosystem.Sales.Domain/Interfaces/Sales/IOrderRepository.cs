using ZAP.Ecosystem.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Sales.Domain.Interfaces;
    public interface IOrderRepository : ZAP.Ecosystem.Shared.Data.IBaseRepository<OrderEntity>
    {
        Task<IEnumerable<OrderEntity>> GetByStatusAsync(string status);
        Task<object> GetOrderSummaryAsync(string status, int page, int pageSize);

        /// <summary>
        /// Get paginated order list with optional keyword (OrderCode) and status filter.
        /// Returns (items, totalCount).
        /// </summary>
        Task<(List<OrderEntity> Items, long TotalCount)> GetOrderListAsync(
            string? keyword, string? status, int pageIndex, int pageSize);

        Task<IReadOnlyList<OrderDetailEntity>> GetRelatedOrderDetailsAsync(IEnumerable<string> orderIds);
        Task<IReadOnlyList<LocationEntity>> GetLocationsAsync(IEnumerable<string> locationGuids);
    }




