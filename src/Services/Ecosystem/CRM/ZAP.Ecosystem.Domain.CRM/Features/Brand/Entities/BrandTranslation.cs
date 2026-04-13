using CRM.BuildingBlocks;

namespace CRM.Brand.Domain.Entities
{
    public class ProductTranslation : BaseTranslationEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
