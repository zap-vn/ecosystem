






using ZAP.CRM.Catalog.Domain.Interfaces.Brands;
using ZAP.CRM.Catalog.Domain.Interfaces.Products;
using ZAP.CRM.Catalog.Domain.Interfaces.Menus;
using ZAP.CRM.Catalog.Domain.Interfaces.Categories;
using ZAP.CRM.Catalog.Domain.Interfaces.Locations;
using ZAP.CRM.Catalog.Domain.Interfaces.Modifiers;
using ZAP.CRM.Catalog.Domain.Interfaces.Geography;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;






using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Menus;
using ZAP.CRM.Catalog.Domain.Entities.Categories;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using ZAP.CRM.Catalog.Domain.Entities.Modifiers;
using ZAP.CRM.Catalog.Domain.Entities.Geography;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using ZAP.CRM.Catalog.Domain.Interfaces;
using ZAP.Ecosystem.Shared.Entities;
using ZAP.CRM.Catalog.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.Infrastructure.Data;
using ZAP.Ecosystem.Shared.Data;

namespace ZAP.CRM.Catalog.Infrastructure.Repositories.Geography;
    public class GeoCountryRepository : BaseRepository<GeoCountry>, IGeoCountryRepository
    {
        public GeoCountryRepository(EcosystemDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<GeoCountry> Items, int Total)> GetPagedAsync(
            int page,
            int pageSize,
            bool? isActive = null,
            string? search = null,
            string sortField = "id",
            bool sortDescending = false)
        {
            IQueryable<GeoCountry> query = _dbSet.AsNoTracking().Include(c => c.Translations);

            if (isActive.HasValue)
                query = query.Where(c => c.is_active == isActive.Value);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(c =>
                    (c.iso_alpha2 != null && c.iso_alpha2.Contains(search)) ||
                    (c.iso_alpha3 != null && c.iso_alpha3.Contains(search)) ||
                    (c.serial_number != null && c.serial_number.Contains(search)));

            var total = await query.CountAsync();

            query = sortField.ToLower() switch
            {
                "iso_alpha2"   => sortDescending ? query.OrderByDescending(c => c.iso_alpha2)   : query.OrderBy(c => c.iso_alpha2),
                "iso_alpha3"   => sortDescending ? query.OrderByDescending(c => c.iso_alpha3)   : query.OrderBy(c => c.iso_alpha3),
                "serial_id"    => sortDescending ? query.OrderByDescending(c => c.serial_id)    : query.OrderBy(c => c.serial_id),
                "created_at"   => sortDescending ? query.OrderByDescending(c => c.created_at)   : query.OrderBy(c => c.created_at),
                _              => sortDescending ? query.OrderByDescending(c => c.id)            : query.OrderBy(c => c.id),
            };

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }
    }



