using ZAP.Ecosystem.Domain.CRM.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Domain.CRM
{
    public interface IModifierGroupRepository : ZAP.Ecosystem.Shared.Data.IBaseRepository<ModifierGroup>
    {
        Task<IEnumerable<ModifierGroup>> GetAllAsync(Guid? tenantId = null);
        Task<ModifierGroup?> GetByIdAsync(Guid id);
        Task<(IEnumerable<ModifierGroup> Items, int Total)> GetPagedAsync(
            int page,
            int pageSize,
            Guid? tenantId = null,
            string? search = null,
            int? statusId = null,
            string? displayType = null,
            string sortField = "name",
            bool sortDescending = false);
        Task CreateAsync(ModifierGroup modifierGroup);
        Task UpdateAsync(ModifierGroup modifierGroup);
        Task DeleteAsync(Guid id);
    }
}

