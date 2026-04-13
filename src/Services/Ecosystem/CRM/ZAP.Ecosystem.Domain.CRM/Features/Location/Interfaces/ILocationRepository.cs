using CRM.Location.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Location.Domain.Interfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Domain.Entities.Location>> GetPagedAsync(LocationListFilter filter);
        Task<int> GetTotalCountAsync(LocationListFilter filter);
        Task<Domain.Entities.Location?> GetByIdAsync(Guid id);
        Task CreateAsync(Domain.Entities.Location location);
        Task CreateStoreAsync(Store store);
        Task UpdateAsync(Domain.Entities.Location location);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<GeoProvince>> GetProvincesAsync(int localeId);
    }
}


