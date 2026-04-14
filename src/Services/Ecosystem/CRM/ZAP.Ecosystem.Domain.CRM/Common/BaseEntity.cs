using ZAP.Ecosystem.Domain.CRM.Common;
namespace ZAP.Ecosystem.Domain.CRM.Common;

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
    List<T> Translations { get; set; }
}

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}
