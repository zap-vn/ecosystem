using ZAP.CRM.Catalog.Domain.Entities.Brands;
using ZAP.CRM.Catalog.Domain.Entities.Products;
using ZAP.CRM.Catalog.Domain.Entities.Locations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZAP.Ecosystem.Shared.Entities;
using System.Collections.Generic;

namespace ZAP.Ecosystem.Finance.Domain.Entities.Reports;
    [Table("report_template", Schema = "platform")]
    public class ReportTemplate : BaseEntity, ILocalizable<ReportTemplateTranslation>
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty; // Fallback
        public string Type { get; set; } = "Revenue"; // Revenue, Stock, Sales, etc.
        public string ConfigurationJson { get; set; } = string.Empty;
        
        public ICollection<ReportTemplateTranslation> Translations { get; set; } = new List<ReportTemplateTranslation>();
    }




