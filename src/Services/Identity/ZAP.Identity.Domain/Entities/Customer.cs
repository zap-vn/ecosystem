using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZAP.Identity.Domain.Entities;

[Table("customer", Schema = "identity")]
public class Customer
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("username")]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Column("password_hash")]
    [MaxLength(255)]
    public string PasswordHash { get; set; } = string.Empty;

    [Column("email")]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Column("is_active")]
    public bool IsActive { get; set; } = true;
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("refresh_token")]
    [MaxLength(255)]
    public string? RefreshToken { get; set; }

    [Column("refresh_token_expiry_time")]
    public DateTime? RefreshTokenExpiryTime { get; set; }
}
