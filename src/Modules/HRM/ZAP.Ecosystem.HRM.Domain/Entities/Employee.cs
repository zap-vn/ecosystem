using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZAP.Ecosystem.Shared.Entities;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZAP.Ecosystem.HRM.Domain.Entities;
    [Table("employee_profile", Schema = "people")]
    public class Employee : BaseEntity, ILocalizable<EmployeeTranslation>
    {
        // --- Suppress BaseEntity PascalCase properties (not in DB and avoid JSON conflict) ---
        [NotMapped, JsonIgnore] public override string? UserGuid { get; set; }
        [NotMapped, JsonIgnore] public new DateTime CreatedAt { get; set; }
        [NotMapped, JsonIgnore] public new DateTime? UpdatedAt { get; set; }
        [NotMapped, JsonIgnore] public new bool IsDeleted { get; set; }
        [NotMapped, JsonIgnore] public new Guid Id { get; set; }

        public Guid id { get; set; }
        public int serial_id { get; set; }
        public Guid? tenant_id { get; set; }
        
        [Column("employee_code")]
        public string employee_code { get; set; } = string.Empty;
        
        [Column("full_name")]
        public string full_name { get; set; } = string.Empty;
        
        [Column("email")]
        public string email { get; set; } = string.Empty;
        
        [Column("dept_id")]
        public Guid? dept_id { get; set; }
        
        public Guid? user_id { get; set; } 
        public int? status_id { get; set; }
        public DateTime? created_at { get; set; } = DateTime.UtcNow;
        
        public ICollection<EmployeeTranslation> Translations { get; set; } = new List<EmployeeTranslation>();

        // Legacy compatibility (avoid JSON conflict)
        [NotMapped, JsonIgnore] public string EmployeeCode { get => employee_code; set => employee_code = value; }
        [NotMapped, JsonIgnore] public string FirstName { get; set; } = string.Empty;
        [NotMapped, JsonIgnore] public string LastName { get; set; } = string.Empty;
        [NotMapped, JsonIgnore] public string Email { get => email; set => email = value; }
        [NotMapped, JsonIgnore] public string Department { get; set; } = string.Empty;
        [NotMapped, JsonIgnore] public Guid? UserId { get => user_id; set => user_id = value; }
    }




