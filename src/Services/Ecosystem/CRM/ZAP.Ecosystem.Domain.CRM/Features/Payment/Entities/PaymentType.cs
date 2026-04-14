using ZAP.Ecosystem.Domain.CRM.Common;
using System.Collections.Generic;

namespace CRM.Payment.Domain.Entities
{
    public class PaymentType : BaseEntity, ILocalizable<TranslatePaymentType>
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public ICollection<TranslatePaymentType> Translations { get; set; } = new List<TranslatePaymentType>();
    }
}
