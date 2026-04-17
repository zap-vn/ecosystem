using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using ZAP.Ecosystem.CRM.Domain.Entities.Promotions;





using ZAP.Ecosystem.Shared.Entities;
using System;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.CRM.Domain.Interfaces.Promotions;
    public interface IPromotionRepository : ZAP.Ecosystem.Shared.Data.IBaseRepository<PromotionEntity>
    {
        Task<ZAP.Ecosystem.Shared.Data.PagedResult<PromotionEntity>> GetPagedAsync(
            int pageIndex, int pageSize,
            Guid? tenantId = null, string? search = null,
            int? statusId = null, int? discountTypeId = null,
            string sortField = "name", bool sortDescending = false);
    }
