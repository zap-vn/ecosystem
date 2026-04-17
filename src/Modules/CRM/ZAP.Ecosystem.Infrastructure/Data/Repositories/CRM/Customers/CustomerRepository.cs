using ZAP.Ecosystem.CRM.Domain.Entities.Customers;
using ZAP.Ecosystem.CRM.Domain.Interfaces.Customers;
using ZAP.Ecosystem.Infrastructure.Data;
using ZAP.Ecosystem.Shared.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM.Customers;

public class CustomerRepository : BaseRepository<CustomerEntity>, ICustomerRepository
{
    private readonly EcosystemDbContext _context;

    public CustomerRepository(EcosystemDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedResult<CustomerEntity>> GetPagedAsync(
        int pageIndex, int pageSize,
        Guid? tenantId = null, string? search = null,
        int? statusId = null, Guid? tierId = null,
        decimal? minTotalSpent = null, decimal? maxTotalSpent = null,
        decimal? minPoints = null, decimal? maxPoints = null,
        string sortField = "full_name", bool sortDescending = false)
    {
        var query = _context.CrmCustomers
            .AsNoTracking()
            .AsQueryable();

        if (tenantId.HasValue)
            query = query.Where(c => c.tenant_id == tenantId);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(c =>
                (c.full_name != null && c.full_name.Contains(search)) ||
                (c.phone_number != null && c.phone_number.Contains(search)) ||
                (c.email != null && c.email.Contains(search)));

        if (statusId.HasValue)
            query = query.Where(c => c.status_id == statusId);

        if (tierId.HasValue)
            query = query.Where(c => c.tier_id == tierId);

        if (minTotalSpent.HasValue)
            query = query.Where(c => c.total_spent_amount >= minTotalSpent);

        if (maxTotalSpent.HasValue)
            query = query.Where(c => c.total_spent_amount <= maxTotalSpent);

        if (minPoints.HasValue)
            query = query.Where(c => c.current_points_balance >= minPoints);

        if (maxPoints.HasValue)
            query = query.Where(c => c.current_points_balance <= maxPoints);

        query = sortDescending
            ? query.OrderByDescending(c => EF.Property<object>(c, MapSortField(sortField)))
            : query.OrderBy(c => EF.Property<object>(c, MapSortField(sortField)));

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<CustomerEntity>(items, totalCount, pageIndex, pageSize);
    }

    private static string MapSortField(string field) => field switch
    {
        "full_name" => nameof(CustomerEntity.full_name),
        "created_at" => nameof(CustomerEntity.created_at),
        "total_spent" => nameof(CustomerEntity.total_spent_amount),
        "points" => nameof(CustomerEntity.current_points_balance),
        _ => nameof(CustomerEntity.full_name)
    };
}
