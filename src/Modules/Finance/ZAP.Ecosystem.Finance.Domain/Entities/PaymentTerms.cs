using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZAP.Ecosystem.Shared.Entities;
using System.Collections.Generic;

namespace ZAP.Ecosystem.Finance.Domain.Entities;
    [Table("payment_terms", Schema = "commerce")]
    public class PaymentTerms : BaseEntity, ILocalizable<PaymentTermsTranslate>
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Days { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<PaymentTermsTranslate> Translations { get; set; } = new List<PaymentTermsTranslate>();
    }




