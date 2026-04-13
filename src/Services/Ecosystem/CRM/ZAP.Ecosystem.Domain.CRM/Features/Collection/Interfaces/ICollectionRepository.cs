using ZAP.Ecosystem.Domain.CRM.Common;
using CRM.Collection.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Collection.Domain.Interfaces
{
    public interface ICollectionRepository
    {
        Task<Domain.Entities.Collection?> GetByIdAsync(Guid id);
        Task<(IEnumerable<Domain.Entities.Collection> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, string? search = null);
        Task CreateAsync(Domain.Entities.Collection collection);
        Task UpdateAsync(Domain.Entities.Collection collection);
        Task DeleteAsync(Guid id);
        Task AddItemsAsync(Guid collectionId, IEnumerable<Guid> productIds);
        Task RemoveItemsAsync(Guid collectionId, IEnumerable<Guid> productIds);
    }
}


