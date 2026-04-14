using ZAP.Ecosystem.Domain.CRM.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Domain.CRM
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetPagedAsync(LocationListFilter filter);
        Task<int> GetTotalCountAsync(LocationListFilter filter);
        Task<Location?> GetByIdAsync(Guid id);
        Task CreateAsync(Location location);
        Task CreateStoreAsync(Store store);
        Task UpdateAsync(Location location);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<GeoProvince>> GetProvincesAsync(int localeId);
    }
}



