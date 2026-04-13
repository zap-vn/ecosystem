using ZAP.Ecosystem.Domain.CRM.Common;
using CRM.Menu.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Menu.Domain.Interfaces
{
    public interface IDiningOptionRepository
    {
        Task<IEnumerable<DiningOption>> GetAllAsync(int localeId);
        Task<DiningOption?> GetByIdAsync(int id, int? localeId);
        Task CreateAsync(DiningOption entity);
        Task UpdateAsync(DiningOption entity);
    }
}

