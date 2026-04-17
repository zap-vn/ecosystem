using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZAP.Ecosystem.Shared.Entities;
using System.Collections.Generic;

namespace ZAP.Ecosystem.Finance.Domain.Entities;
    [Table("payment_type", Schema = "commerce")]
    public class PaymentType : BaseEntity, ILocalizable<TranslatePaymentType>
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public ICollection<TranslatePaymentType> Translations { get; set; } = new List<TranslatePaymentType>();
    }




