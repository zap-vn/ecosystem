using ZAP.Ecosystem.Domain.CRM.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Domain.CRM
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee?> GetByCodeAsync(string code);
        
        // i18n support
        Task<EmployeeTranslation?> GetTranslationAsync(string employeeId, string languageCode);
        Task UpsertTranslationAsync(EmployeeTranslation translation);
    }
}

