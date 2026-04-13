using CRM.DiningOption.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.DiningOption.Domain.Interfaces
{
    public interface IDiningOptionRepository
    {
        Task<IEnumerable<Domain.Entities.DiningOption>> GetAllAsync(int localeId);
        Task<Domain.Entities.DiningOption?> GetByIdAsync(int id, int? localeId);
        Task CreateAsync(Domain.Entities.DiningOption entity);
        Task UpdateAsync(Domain.Entities.DiningOption entity);
    }
}


