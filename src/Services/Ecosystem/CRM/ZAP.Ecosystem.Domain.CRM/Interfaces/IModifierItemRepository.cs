using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Domain.CRM
{
    public interface IModifierItemRepository
    {
        Task<IEnumerable<ModifierItem>> GetAllAsync(Guid? groupId = null);
        Task<ModifierItem?> GetByIdAsync(Guid id);
        Task<(IEnumerable<ModifierItem> Items, int Total)> GetPagedAsync(
            int page,
            int pageSize,
            Guid? groupId = null,
            int? statusId = null,
            string sortField = "sort_order",
            bool sortDescending = false);
    }
}
