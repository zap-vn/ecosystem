using ZAP.Ecosystem.Domain.CRM.Common;

namespace ZAP.Ecosystem.Domain.CRM
{
    public class CustomerTranslation : BaseTranslationEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}

