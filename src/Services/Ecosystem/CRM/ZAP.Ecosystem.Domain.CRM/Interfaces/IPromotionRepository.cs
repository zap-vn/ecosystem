using System;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM.Common;

namespace ZAP.Ecosystem.Domain.CRM
{
    public interface IPromotionRepository : ZAP.Ecosystem.Shared.Data.IBaseRepository<PromotionEntity>
    {
        Task<ZAP.Ecosystem.Shared.Data.PagedResult<PromotionEntity>> GetPagedAsync(
            int pageIndex, int pageSize,
            Guid? tenantId = null, string? search = null,
            int? statusId = null, int? discountTypeId = null,
            string sortField = "name", bool sortDescending = false);
    }
}

