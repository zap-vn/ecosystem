using ZAP.Ecosystem.Domain.CRM.Common;

namespace CRM.Promotion.Domain.Entities
{
    public class PromotionTranslation : BaseTranslationEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
