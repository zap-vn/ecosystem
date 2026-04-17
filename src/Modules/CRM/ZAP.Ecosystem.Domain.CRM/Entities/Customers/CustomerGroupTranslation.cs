using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Locations;








using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZAP.Ecosystem.Shared.Entities;

namespace ZAP.Ecosystem.CRM.Domain.Entities.Customers;
    [Table("customer_group_translation", Schema = "people")]
    public class CustomerGroupTranslation : BaseTranslationEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
