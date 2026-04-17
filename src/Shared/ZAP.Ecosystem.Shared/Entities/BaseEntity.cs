using System;
using System.Collections.Generic;

namespace ZAP.Ecosystem.Shared.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public virtual string? UserGuid { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}

public abstract class BaseTranslationEntity
{
    public Guid Id { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
}

public interface ILocalizable<T>
{
    ICollection<T> Translations { get; set; }
}


