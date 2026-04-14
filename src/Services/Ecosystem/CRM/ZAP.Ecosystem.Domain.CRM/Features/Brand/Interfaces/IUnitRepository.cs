using ZAP.Ecosystem.Domain.CRM.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Brand.Domain.Entities;

namespace CRM.Brand.Domain.Interfaces
{
    public interface IUnitRepository
    {
        Task<IEnumerable<UomItem>> GetAllAsync(Guid? tenantId = null);
        Task<UomItem?> GetByIdAsync(int id);
        Task<(IEnumerable<UomItem> Items, int Total)> GetPagedAsync(
            int page,
            int pageSize,
            Guid? tenantId = null,
            string? search = null,
            int? statusId = null,
            int? precision = null,
            string sortField = "name",
            bool sortDescending = false);
        Task CreateAsync(UomItem uomItem);
        Task UpdateAsync(UomItem uomItem);
        Task DeleteAsync(int id);
    }
}
