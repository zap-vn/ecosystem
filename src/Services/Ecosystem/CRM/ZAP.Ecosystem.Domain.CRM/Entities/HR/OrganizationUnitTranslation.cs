using ZAP.Ecosystem.Domain.CRM.Common;

namespace ZAP.Ecosystem.Domain.CRM
{
    public class OrganizationUnitTranslation : BaseTranslationEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

