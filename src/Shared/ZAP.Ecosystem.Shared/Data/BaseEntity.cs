using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZAP.Ecosystem.Shared.Data;

public abstract class BaseEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [NotMapped]
    public string? CreatedBy { get; set; }

    [NotMapped]
    public string? UpdatedBy { get; set; }

    [NotMapped]
    public bool IsDeleted { get; set; }
}

public abstract class BaseTranslationEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [NotMapped]
    [Column("language_code")]
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
