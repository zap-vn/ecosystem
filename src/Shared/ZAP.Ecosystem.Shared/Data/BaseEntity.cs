using System;
using System.Collections.Generic;

namespace ZAP.Ecosystem.Shared.Data;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}

public abstract class BaseTranslationEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string LanguageCode { get; set; } = "en";
}

public interface ILocalizable<TTranslation> where TTranslation : BaseTranslationEntity
{
    ICollection<TTranslation> Translations { get; set; }
}

public abstract class FilterDTOs
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Keyword { get; set; }
}


