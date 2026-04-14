using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class PaymentTypeRepository : IPaymentTypeRepository
    {
        private readonly EcosystemDbContext _context;
        public PaymentTypeRepository(EcosystemDbContext context) => _context = context;
        public Task<PaymentType> GetByIdAsync(Guid id) => throw new NotImplementedException();
        public Task<IEnumerable<PaymentType>> GetAllAsync() => throw new NotImplementedException();
        public Task AddAsync(PaymentType entity) => throw new NotImplementedException();
        public Task UpdateAsync(PaymentType entity) => throw new NotImplementedException();
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();
    }
}
