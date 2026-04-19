using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class PromotionRepository : ZAP.Ecosystem.Shared.Data.BaseRepository<PromotionEntity>, IPromotionRepository
    {
<<<<<<< HEAD
        public PromotionRepository(EcosystemDbContext context) : base(context) { }
        public Task<ZAP.Ecosystem.Shared.Data.PagedResult<PromotionEntity>> GetPagedAsync(int pageIndex, int pageSize, Guid? tenantId = null, string? search = null, int? statusId = null, int? discountTypeId = null, string sortField = "name", bool sortDescending = false) => throw new NotImplementedException();
=======
        private readonly EcosystemDbContext _context;
        public PromotionRepository(EcosystemDbContext context) => _context = context;
        public Task<PromotionEntity> GetByIdAsync(Guid id) => throw new NotImplementedException();
        public Task<IEnumerable<PromotionEntity>> GetAllAsync() => throw new NotImplementedException();
        public Task AddAsync(PromotionEntity entity) => throw new NotImplementedException();
        public Task UpdateAsync(PromotionEntity entity) => throw new NotImplementedException();
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();
        public Task<ZAP.Ecosystem.Shared.Data.PagedResult<PromotionEntity>> GetPagedAsync(
            int pageIndex, int pageSize,
            Guid? tenantId = null, string? search = null,
            int? statusId = null, int? discountTypeId = null,
            string sortField = "name", bool sortDescending = false) => throw new NotImplementedException();
>>>>>>> 2d05c185d05501e05ba3d75aa9388bc42e71d197
    }
}
