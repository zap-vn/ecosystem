using ZAP.Ecosystem.Domain.CRM.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZAP.Ecosystem.Domain.CRM
{
    public class TaxSyncSetting
    {
        public bool IsAutoSyncEnabled { get; set; }
        public int SettingTypeId { get; set; }
        public string TimePicker { get; set; } = string.Empty;
    }

    [Table("customer", Schema = "people")]
    public class CustomerEntity : BaseEntity, ILocalizable<CustomerTranslation>
    {
        // --- Legacy fields (not in DB) ---
        [NotMapped] public long _key { get; set; }
        [NotMapped] public string _rev { get; set; } = string.Empty;
        [NotMapped] public string BusinessName { get; set; } = string.Empty;
        [NotMapped] public string BussinessTypeId { get; set; } = string.Empty;
        [NotMapped] public int Country { get; set; }
        [NotMapped] public string CreateDate { get; set; } = string.Empty;
        [NotMapped] public string CurrencyId { get; set; } = string.Empty;
        [NotMapped] public string CurrencyNativeName { get; set; } = string.Empty;
        [NotMapped] public string CurrencySymbol { get; set; } = string.Empty;
        [NotMapped] public string CustomerCode { get; set; } = string.Empty;
        [NotMapped] public long LanguageId { get; set; }
        [NotMapped] public string Language { get; set; } = string.Empty;
        [NotMapped] public int CustomerStatusId { get; set; }
        [NotMapped] public string Email { get; set; } = string.Empty;
        [NotMapped] public string EmpGuid { get; set; } = string.Empty;
        [NotMapped] public string FirstName { get; set; } = string.Empty;
        [NotMapped] public string InterestGrade { get; set; } = string.Empty;
        [NotMapped] public string LastName { get; set; } = string.Empty;
        [NotMapped] public string PassCode { get; set; } = string.Empty;
        [NotMapped] public string Password { get; set; } = string.Empty;
        [NotMapped] public string Phone { get; set; } = string.Empty;
        [NotMapped] public string Point { get; set; } = string.Empty;
        [NotMapped] public string ReferenceId { get; set; } = string.Empty;
        [NotMapped] public string StartedDate { get; set; } = string.Empty;
        [NotMapped] public string TimeZoneDisplayName { get; set; } = string.Empty;
        [NotMapped] public string TimeZoneId { get; set; } = string.Empty;
        [NotMapped] public int Visible { get; set; }
        [NotMapped] public string Websites { get; set; } = string.Empty;
        [NotMapped] public string MerchantUrl { get; set; } = string.Empty;
        [NotMapped] public int Currency_key { get; set; }
        [NotMapped] public string Plural { get; set; } = string.Empty;
        [NotMapped] public string Singular { get; set; } = string.Empty;
        [NotMapped] public int NotificationId { get; set; }
        [NotMapped] public string PublicKey { get; set; } = string.Empty;
        [NotMapped] public string AdminUpdate { get; set; } = string.Empty;
        [NotMapped] public string BatchCode { get; set; } = string.Empty;
        [NotMapped] public string LinkVAT { get; set; } = string.Empty;
        [NotMapped] public int TimeReport { get; set; }
        [NotMapped] public int TimeInvoice { get; set; }
        [NotMapped] public TaxSyncSetting TaxSyncSetting { get; set; } = new TaxSyncSetting();
        [NotMapped] public string MerchantName { get; set; } = string.Empty;
        [NotMapped] public string RegistrationSource { get; set; } = "Email";
        [NotMapped] public bool IsVerify { get; set; } = false;
        [NotMapped] public bool IsVerifyPhone { get; set; } = false;
        [NotMapped] public bool IsVerifyEmail { get; set; } = false;
        [NotMapped] public bool IsVerifyGoogle { get; set; } = false;
        [NotMapped] public bool IsVerifyApple { get; set; } = false;

        // --- Postgres Specific Fields ---
        public Guid id { get; set; }
        public int serial_id { get; set; }
        public string serial_number { get; set; } = string.Empty;
        public Guid? tenant_id { get; set; }
        public string customer_code { get; set; } = string.Empty;
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
        public string creation_source { get; set; } = "Email";
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

        public virtual LoyaltyTier? loyalty_tier { get; set; }
        public virtual StatusItem? status { get; set; }
        [NotMapped] public string? status_code { get; set; }
        [NotMapped] public string? status_name { get; set; }
        // --------------------------------

        // Legacy aliases (not in DB)
        [NotMapped] public string Name { get; set; } = string.Empty;
        [NotMapped] public string PhoneNumber { get; set; } = string.Empty;
        [NotMapped] public string Address { get; set; } = string.Empty;
        [NotMapped] public bool IsActive { get; set; } = true;

        public ICollection<CustomerTranslation> Translations { get; set; } = new List<CustomerTranslation>();
    }
}
