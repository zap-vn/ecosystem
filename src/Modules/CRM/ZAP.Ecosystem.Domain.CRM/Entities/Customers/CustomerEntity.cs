using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Locations;








using ZAP.Ecosystem.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZAP.Ecosystem.CRM.Domain.Entities.Customers;
    [Table("tax_sync_setting", Schema = "platform")]
    [Keyless]
    public class TaxSyncSetting
    {
        public bool IsAutoSyncEnabled { get; set; }
        public int SettingTypeId { get; set; }
        public string TimePicker { get; set; } = string.Empty;
    }

    [Table("customer", Schema = "people")]
    public class CustomerEntity : BaseEntity, ILocalizable<CustomerTranslation>
    {


        // --- Postgres Specific Fields ---
        public Guid id { get; set; }
        public int serial_id { get; set; }
        public string? serial_number { get; set; }
        public Guid? tenant_id { get; set; }
        public string? customer_code { get; set; }
        public string? legacy_id { get; set; }
        public string? square_customer_id { get; set; }
        public string? reference_id { get; set; }
        public string? phone_number { get; set; }
        public string? email { get; set; }
        public string? full_name { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? nickname { get; set; }
        public string? company_name { get; set; }
        public string? avatar_url { get; set; }
        public int? gender_id { get; set; }
        public DateTime? birth_date { get; set; }
        public string? address_line_1 { get; set; }
        public string? address_line_2 { get; set; }
        public string? city_name { get; set; }
        public string? state_name { get; set; }
        public int? country_id { get; set; }
        public int? province_id { get; set; }
        public int? district_id { get; set; }
        public int? ward_id { get; set; }
        public string? zipcode { get; set; }
        public int? preferred_locale_id { get; set; }
        public Guid? user_id { get; set; }
        public Guid? tier_id { get; set; }
        public string? memo { get; set; }
        public string? creation_source { get; set; }
        public string? email_subscription_status { get; set; }
        public bool is_instant_profile { get; set; } = false;
        public decimal current_points_balance { get; set; } = 0;
        public decimal total_spent_amount { get; set; } = 0;
        public decimal average_spent_amount { get; set; } = 0;
        public int total_visits_count { get; set; } = 0;
        public DateTime? first_visit_at { get; set; }
        public DateTime? last_visit_at { get; set; }
        public int? status_id { get; set; }
        [NotMapped] public string? display_initial { get; set; }
        public Guid? group_id { get; set; }
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime? updated_at { get; set; }

        [NotMapped] public virtual LoyaltyTier? loyalty_tier { get; set; }
        [NotMapped] public virtual StatusItem? status { get; set; }
        [NotMapped] public string? status_code { get; set; }
        [NotMapped] public string? status_name { get; set; }
        // --------------------------------



        public ICollection<CustomerTranslation> Translations { get; set; } = new List<CustomerTranslation>();
    }

