using ZAP.Ecosystem.Domain.CRM.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Brand.Domain.Entities;

namespace CRM.Brand.Domain.Interfaces
{
    public interface IModifierGroupRepository
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
