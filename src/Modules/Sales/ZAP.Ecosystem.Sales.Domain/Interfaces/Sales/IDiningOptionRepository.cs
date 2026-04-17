using ZAP.Ecosystem.Shared.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Sales.Domain.Interfaces;
    public interface IDiningOptionRepository : ZAP.Ecosystem.Shared.Data.IBaseRepository<DiningOption>
    {
        Task<IEnumerable<DiningOption>> GetAllAsync(int localeId);
        Task<DiningOption?> GetByIdAsync(int id, int? localeId);
    }




