using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly EcosystemDbContext _context;
        public PaymentMethodRepository(EcosystemDbContext context) => _context = context;
        public Task<PaymentMethod> GetByIdAsync(Guid id) => throw new NotImplementedException();
        public Task<IEnumerable<PaymentMethod>> GetAllAsync() => throw new NotImplementedException();
        public Task AddAsync(PaymentMethod entity) => throw new NotImplementedException();
        public Task UpdateAsync(PaymentMethod entity) => throw new NotImplementedException();
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();
    }
}
