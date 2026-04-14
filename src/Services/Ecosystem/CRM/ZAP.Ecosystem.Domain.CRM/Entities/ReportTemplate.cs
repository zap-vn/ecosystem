using ZAP.Ecosystem.Domain.CRM.Common;
using System.Collections.Generic;

namespace ZAP.Ecosystem.Domain.CRM
{
    public class ReportTemplate : BaseEntity, ILocalizable<ReportTemplateTranslation>
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty; // Fallback
        public string Type { get; set; } = "Revenue"; // Revenue, Stock, Sales, etc.
        public string ConfigurationJson { get; set; } = string.Empty;
        
        public ICollection<ReportTemplateTranslation> Translations { get; set; } = new List<ReportTemplateTranslation>();
    }
}

