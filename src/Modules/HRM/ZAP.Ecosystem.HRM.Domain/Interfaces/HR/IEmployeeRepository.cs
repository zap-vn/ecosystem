using ZAP.Ecosystem.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.HRM.Domain.Interfaces;
    public interface IEmployeeRepository : ZAP.Ecosystem.Shared.Data.IBaseRepository<Employee>
    {
        Task<Employee?> GetByCodeAsync(string code);
        
        // i18n support
        Task<EmployeeTranslation?> GetTranslationAsync(string employeeId, string languageCode);
        Task UpsertTranslationAsync(EmployeeTranslation translation);
    }




