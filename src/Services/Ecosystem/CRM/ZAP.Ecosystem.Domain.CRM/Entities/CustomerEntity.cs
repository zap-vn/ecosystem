using ZAP.Ecosystem.Domain.CRM.Common;
using System.Collections.Generic;

namespace ZAP.Ecosystem.Domain.CRM
{
    public class TaxSyncSetting
    {
        public bool IsAutoSyncEnabled { get; set; }
        public int SettingTypeId { get; set; }
        public string TimePicker { get; set; } = string.Empty;
    }

        public class CustomerEntity : BaseEntity, ILocalizable<CustomerTranslation>
    {
        public long _key { get; set; }
        public string _rev { get; set; } = string.Empty;
        public string BusinessName { get; set; } = string.Empty;
        public string BussinessTypeId { get; set; } = string.Empty;
        public int Country { get; set; }
        public string CreateDate { get; set; } = string.Empty;
        public string CurrencyId { get; set; } = string.Empty;
        public string CurrencyNativeName { get; set; } = string.Empty;
        public string CurrencySymbol { get; set; } = string.Empty;
        public string CustomerCode { get; set; } = string.Empty;
        
        public long LanguageId { get; set; }
        
                public string Language { get; set; } = string.Empty;
        
        public int CustomerStatusId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string EmpGuid { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string InterestGrade { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PassCode { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Point { get; set; } = string.Empty;
        public string ReferenceId { get; set; } = string.Empty;
        public string StartedDate { get; set; } = string.Empty;
        public string TimeZoneDisplayName { get; set; } = string.Empty;
        public string TimeZoneId { get; set; } = string.Empty;
        public int Visible { get; set; }
        public string Websites { get; set; } = string.Empty;
        
                public string MerchantUrl { get; set; } = string.Empty;
        public int Currency_key { get; set; }
        public string Plural { get; set; } = string.Empty;
        public string Singular { get; set; } = string.Empty;
        public int NotificationId { get; set; }
        public string PublicKey { get; set; } = string.Empty;
        public string AdminUpdate { get; set; } = string.Empty;
        public string BatchCode { get; set; } = string.Empty;
        public string LinkVAT { get; set; } = string.Empty;
        public int TimeReport { get; set; }
        public int TimeInvoice { get; set; }
        public TaxSyncSetting TaxSyncSetting { get; set; } = new TaxSyncSetting();
        public string MerchantName { get; set; } = string.Empty;
        public string RegistrationSource { get; set; } = "Email";
        
        public bool IsVerify { get; set; } = false;
        public bool IsVerifyPhone { get; set; } = false;
        public bool IsVerifyEmail { get; set; } = false;
        public bool IsVerifyGoogle { get; set; } = false;
        public bool IsVerifyApple { get; set; } = false;
        
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
                public string? display_initial { get; set; }
                public Guid? group_id { get; set; }
                public DateTime created_at { get; set; } = DateTime.UtcNow;
                public DateTime? updated_at { get; set; }

                public virtual LoyaltyTier? loyalty_tier { get; set; }

                public virtual StatusItem? status { get; set; }
                public string? status_code { get; set; }
                public string? status_name { get; set; }
        // -------------------------------

        // Legacy fields for compilation if needed
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        
        public ICollection<CustomerTranslation> Translations { get; set; } = new List<CustomerTranslation>();
    }
}

