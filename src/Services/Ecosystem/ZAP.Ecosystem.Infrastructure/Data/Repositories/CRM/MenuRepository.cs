using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class MenuRepository : ZAP.Ecosystem.Shared.Data.BaseRepository<MenuHeader>, IMenuRepository
    {
        public MenuRepository(EcosystemDbContext context) : base(context) { }

        public Task<MenuHeader> GetByIdAsync(Guid id) => throw new NotImplementedException();
        public Task<(IEnumerable<MenuHeader> Items, int Total)> GetPagedAsync(int page, int pageSize, Guid? tenantId = null, string? search = null, bool? isActive = null, string? menuType = null, int localeId = 2, string sortField = "name", bool sortDescending = false) => throw new NotImplementedException();
        public Task CreateAsync(MenuHeader menu) => throw new NotImplementedException();
        public Task UpdateAsync(MenuHeader menu) => throw new NotImplementedException();
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();
    }
}





