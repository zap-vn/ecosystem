using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class CollectionRepository : ZAP.Ecosystem.Shared.Data.BaseRepository<Collection>, ICollectionRepository
    {
        public CollectionRepository(EcosystemDbContext context) : base(context) { }

        public Task<Collection> GetByIdAsync(Guid id) => throw new NotImplementedException();
        public Task<(IEnumerable<Collection> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, string search = null) => throw new NotImplementedException();
        public Task CreateAsync(Collection collection) => throw new NotImplementedException();
        public Task UpdateAsync(Collection collection) => throw new NotImplementedException();
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();
        public Task AddItemsAsync(Guid collectionId, IEnumerable<Guid> productIds) => throw new NotImplementedException();
        public Task RemoveItemsAsync(Guid collectionId, IEnumerable<Guid> productIds) => throw new NotImplementedException();
    }
}
