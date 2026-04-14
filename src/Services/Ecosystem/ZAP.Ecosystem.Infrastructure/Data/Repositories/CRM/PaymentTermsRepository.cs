using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZAP.Ecosystem.Domain.CRM;
using ZAP.Ecosystem.Infrastructure.Data;

namespace ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM
{
    public class PaymentTermsRepository : IPaymentTermsRepository
    {
        private readonly EcosystemDbContext _context;
        public PaymentTermsRepository(EcosystemDbContext context) => _context = context;
        public Task<PaymentTerms> GetByIdAsync(Guid id) => throw new NotImplementedException();
        public Task<IEnumerable<PaymentTerms>> GetAllAsync() => throw new NotImplementedException();
        public Task AddAsync(PaymentTerms entity) => throw new NotImplementedException();
        public Task UpdateAsync(PaymentTerms entity) => throw new NotImplementedException();
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();
    }
}
