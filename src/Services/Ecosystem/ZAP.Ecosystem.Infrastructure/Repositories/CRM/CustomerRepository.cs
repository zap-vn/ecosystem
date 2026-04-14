using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;
using ZAP.Ecosystem.Shared.Data;

namespace ZAP.Ecosystem.Infrastructure.Repositories.CRM
{
    public class CustomerRepository : BaseRepository<CustomerEntity>, ICustomerRepository
    {
        public CustomerRepository(EcosystemDbContext context) : base(context)
        {
        }

        public async Task<PagedResult<CustomerEntity>> GetPagedAsync(
            int pageIndex, int pageSize,
            Guid? tenantId = null, string? search = null,
            int? statusId = null, Guid? tierId = null,
            decimal? minTotalSpent = null, decimal? maxTotalSpent = null,
            decimal? minPoints = null, decimal? maxPoints = null,
            string sortField = "full_name", bool sortDescending = false)
        {
            var query = _dbContext.Set<CustomerEntity>()
                .Include(c => c.status)
                .ThenInclude(s => s.translations)
                .Include(c => c.loyalty_tier)
                .AsNoTracking();

            // Filters
            if (tenantId.HasValue)
                query = query.Where(c => c.tenant_id == tenantId.Value);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.full_name.Contains(search) || c.email.Contains(search) || c.phone_number.Contains(search));
            }

            if (statusId.HasValue)
                query = query.Where(c => c.status_id == statusId.Value);

            if (tierId.HasValue)
                query = query.Where(c => c.tier_id == tierId.Value);

            if (minTotalSpent.HasValue)
                query = query.Where(c => c.total_spent_amount >= minTotalSpent.Value);

            if (maxTotalSpent.HasValue)
                query = query.Where(c => c.total_spent_amount <= maxTotalSpent.Value);

            var totalCount = await query.CountAsync();

            // Sort
            query = sortField.ToLower() switch
            {
                "full_name" => sortDescending ? query.OrderByDescending(c => c.full_name) : query.OrderBy(c => c.full_name),
                "email" => sortDescending ? query.OrderByDescending(c => c.email) : query.OrderBy(c => c.email),
                "phone_number" => sortDescending ? query.OrderByDescending(c => c.phone_number) : query.OrderBy(c => c.phone_number),
                "total_spent_amount" => sortDescending ? query.OrderByDescending(c => c.total_spent_amount) : query.OrderBy(c => c.total_spent_amount),
                "current_points_balance" => sortDescending ? query.OrderByDescending(c => c.current_points_balance) : query.OrderBy(c => c.current_points_balance),
                _ => sortDescending ? query.OrderByDescending(c => c.full_name) : query.OrderBy(c => c.full_name)
            };

            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<CustomerEntity>(items, totalCount, pageIndex, pageSize);
        }

        public async Task<CustomerEntity?> GetByIdAsync(string id)
        {
            if (Guid.TryParse(id, out var guid))
                return await _dbSet.FindAsync(guid);
            return null;
        }

        public async Task<CustomerEntity> CreateAsync(CustomerEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(CustomerEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;
            _dbSet.Remove(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
