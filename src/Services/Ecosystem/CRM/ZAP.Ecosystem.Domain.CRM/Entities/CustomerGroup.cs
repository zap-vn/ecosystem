using ZAP.Ecosystem.Domain.CRM.Common;
using System.Collections.Generic;

namespace ZAP.Ecosystem.Domain.CRM
{
    public class CustomerGroup : BaseEntity, ILocalizable<CustomerGroupTranslation>
    {
        public string Name { get; set; } = string.Empty; // Fallback
        public string Description { get; set; } = string.Empty; // Fallback
        public decimal DiscountPercentage { get; set; }
        
        public ICollection<CustomerGroupTranslation> Translations { get; set; } = new List<CustomerGroupTranslation>();
    }
}

