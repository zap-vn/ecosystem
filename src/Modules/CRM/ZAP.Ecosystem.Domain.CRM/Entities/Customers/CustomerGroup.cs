using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Locations;








using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZAP.Ecosystem.Shared.Entities;
using System.Collections.Generic;

namespace ZAP.Ecosystem.CRM.Domain.Entities.Customers;
    [Table("customer_group", Schema = "people")]
    public class CustomerGroup : BaseEntity, ILocalizable<CustomerGroupTranslation>
    {
        public string Name { get; set; } = string.Empty; // Fallback
        public string Description { get; set; } = string.Empty; // Fallback
        public decimal DiscountPercentage { get; set; }
        
        public ICollection<CustomerGroupTranslation> Translations { get; set; } = new List<CustomerGroupTranslation>();
    }
