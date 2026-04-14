using ZAP.Ecosystem.Domain.CRM.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Sales.Domain.Entities.Products;

namespace CRM.Sales.Domain.Interfaces
{
    public interface IProductRepository : IRepository<ProductEntity>
    {
        Task<IEnumerable<ProductEntity>> GetByCategoryAsync(string category);
    }
}
