using ZAP.Ecosystem.Domain.CRM.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Domain.CRM
{
    public interface IOrderRepository : IRepository<OrderEntity>
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
}

