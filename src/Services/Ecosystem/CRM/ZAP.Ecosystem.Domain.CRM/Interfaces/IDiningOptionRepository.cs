using ZAP.Ecosystem.Domain.CRM.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Domain.CRM
{
    public interface IDiningOptionRepository
    {
        Task<IEnumerable<DiningOption>> GetAllAsync(int localeId);
        Task<DiningOption?> GetByIdAsync(int id, int? localeId);
        Task CreateAsync(DiningOption entity);
        Task UpdateAsync(DiningOption entity);
    }
}



