using ZAP.Ecosystem.Domain.CRM.Common;

namespace CRM.HR.Domain.Entities
{
    public class EmployeeTranslation : BaseTranslationEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
    }
}
