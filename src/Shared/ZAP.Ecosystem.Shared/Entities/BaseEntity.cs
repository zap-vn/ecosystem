using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZAP.Ecosystem.Shared.Entities;

public abstract class BaseEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    [NotMapped]
    public virtual string? UserGuid { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [NotMapped]
    public bool IsDeleted { get; set; } = false;
}

public abstract class BaseTranslationEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [NotMapped]
    [Column("language_code")]
    public string LanguageCode { get; set; } = string.Empty;
}

public interface ILocalizable<T>
{
    ICollection<T> Translations { get; set; }
}
