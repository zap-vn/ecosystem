using ZAP.Ecosystem.Domain.CRM.Common;

namespace CRM.Payment.Domain.Entities
{
    public class PaymentTermsTranslate : BaseTranslationEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
