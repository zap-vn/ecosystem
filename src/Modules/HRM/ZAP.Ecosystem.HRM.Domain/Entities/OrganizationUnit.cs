using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZAP.Ecosystem.Shared.Entities;
using System;
using System.Collections.Generic;

namespace ZAP.Ecosystem.HRM.Domain.Entities;
    [Table("organization_unit", Schema = "people")]
    public class OrganizationUnit : BaseEntity, ILocalizable<OrganizationUnitTranslation>
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty; // Fallback
        public string Description { get; set; } = string.Empty; // Fallback
        public Guid? ParentId { get; set; }
        public string Type { get; set; } = "Department"; // Company, Branch, Department, Team
        
        public ICollection<OrganizationUnitTranslation> Translations { get; set; } = new List<OrganizationUnitTranslation>();
    }




