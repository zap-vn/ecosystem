using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZAP.Ecosystem.Shared.Entities;
using System.Collections.Generic;

namespace ZAP.Ecosystem.Finance.Domain.Entities;
    [Table("payment_method", Schema = "commerce")]
    public class PaymentMethod : BaseEntity, ILocalizable<PaymentMethodTranslation>
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty; // Fallback
        public string Icon { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        
        public ICollection<PaymentMethodTranslation> Translations { get; set; } = new List<PaymentMethodTranslation>();
    }




