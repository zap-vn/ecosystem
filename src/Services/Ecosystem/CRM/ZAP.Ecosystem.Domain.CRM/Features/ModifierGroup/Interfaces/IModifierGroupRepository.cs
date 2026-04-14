using ZAP.Ecosystem.Domain.CRM.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.ModifierGroup.Domain.Entities;

namespace CRM.ModifierGroup.Domain.Interfaces
{
    public interface IModifierGroupRepository
    {
        Task<IEnumerable<Entities.ModifierGroup>> GetAllAsync(Guid? tenantId = null);
        Task<Entities.ModifierGroup?> GetByIdAsync(Guid id);
        Task<(IEnumerable<Entities.ModifierGroup> Items, int Total)> GetPagedAsync(
            int page,
            int pageSize,
            Guid? tenantId = null,
            string? search = null,
            int? statusId = null,
            string? displayType = null,
            string sortField = "name",
            bool sortDescending = false);
        Task CreateAsync(Entities.ModifierGroup modifierGroup);
        Task UpdateAsync(Entities.ModifierGroup modifierGroup);
        Task DeleteAsync(Guid id);
    }
}
