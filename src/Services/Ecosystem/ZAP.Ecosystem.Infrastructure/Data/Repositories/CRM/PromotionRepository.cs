using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class PromotionRepository : ZAP.Ecosystem.Shared.Data.BaseRepository<PromotionEntity>, IPromotionRepository
    {
        public PromotionRepository(EcosystemDbContext context) : base(context) { }
        public Task<ZAP.Ecosystem.Shared.Data.PagedResult<PromotionEntity>> GetPagedAsync(int pageIndex, int pageSize, Guid? tenantId = null, string? search = null, int? statusId = null, int? discountTypeId = null, string sortField = "name", bool sortDescending = false) => throw new NotImplementedException();
    }
}
