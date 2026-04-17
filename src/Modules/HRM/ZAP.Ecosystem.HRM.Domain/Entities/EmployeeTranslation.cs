using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZAP.Ecosystem.Shared.Entities;

namespace ZAP.Ecosystem.HRM.Domain.Entities;
    [Table("employee_translation", Schema = "people")]
    [PrimaryKey(nameof(Id), nameof(LanguageCode))]
    public class EmployeeTranslation : BaseTranslationEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
    }









