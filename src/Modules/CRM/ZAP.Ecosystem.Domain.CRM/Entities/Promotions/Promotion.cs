using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Locations;








using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZAP.Ecosystem.Shared.Entities;
using System;
using System.Collections.Generic;

namespace ZAP.Ecosystem.CRM.Domain.Entities.Promotions;
    [Table("promotion", Schema = "marketing")]
    public class Promotion : BaseEntity, ILocalizable<PromotionTranslation>
    {
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty; // Fallback
        public string Description { get; set; } = string.Empty; // Fallback
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountType { get; set; } = "Percentage"; // Percentage, FixedAmount
        
        public ICollection<PromotionTranslation> Translations { get; set; } = new List<PromotionTranslation>();
    }
