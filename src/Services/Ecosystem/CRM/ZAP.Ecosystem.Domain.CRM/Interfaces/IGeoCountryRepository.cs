using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Domain.CRM
{
    public interface IGeoCountryRepository
    {
        Task<(IEnumerable<GeoCountry> Items, int Total)> GetPagedAsync(
            int page,
            int pageSize,
            bool? isActive = null,
            string? search = null,
            string sortField = "id",
            bool sortDescending = false);
    }
}
