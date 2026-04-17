using Microsoft.EntityFrameworkCore;
using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZAP.Ecosystem.Shared.Entities;

namespace ZAP.Ecosystem.Finance.Domain.Entities;
    [Table("payment_terms_translate", Schema = "commerce")]
    [PrimaryKey(nameof(Id), nameof(LanguageCode))]
    public class PaymentTermsTranslate : BaseTranslationEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }










