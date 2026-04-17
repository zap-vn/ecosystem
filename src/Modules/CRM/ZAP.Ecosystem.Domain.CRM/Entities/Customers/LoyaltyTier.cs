using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Locations;








using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZAP.Ecosystem.Shared.Entities;
using System;
using System.Collections.Generic;

namespace ZAP.Ecosystem.CRM.Domain.Entities.Customers;
    [Table("loyalty_tier", Schema = "people")]
    public class LoyaltyTier
    {
        public Guid id { get; set; }
        public string tier_name { get; set; } = string.Empty;
        public string? tier_code { get; set; }
        public int? priority_level { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool is_active { get; set; } = true;
        public DateTime? created_at { get; set; } = DateTime.UtcNow;
        public DateTime? updated_at { get; set; } = DateTime.UtcNow;

        // Navigation
        public virtual ICollection<CustomerEntity> customers { get; set; } = new List<CustomerEntity>();
    }
