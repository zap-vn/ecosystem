using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZAP.Identity.Domain.Entities;

[Table("user", Schema = "identity")]
public class User
{
    [Key]
    [Column("id")]
    public Guid id { get; set; }

    [Column("serial_id")]
    public int serial_id { get; set; }

    [Column("serial_number")]
    [MaxLength(100)]
    public string? serial_number { get; set; }

    [Column("legacy_id")]
    public long? legacy_id { get; set; }

    [Column("username")]
    [MaxLength(100)]
    public string? username { get; set; }

    [Column("email")]
    [MaxLength(150)]
    public string? email { get; set; }

    [Column("password_hash")]
    [MaxLength(500)]
    public string password_hash { get; set; } = string.Empty;

    [Column("full_name")]
    [MaxLength(200)]
    public string? full_name { get; set; }

    [Column("status_id")]
    public int status_id { get; set; } = 9001;

    [Column("tenant_id")]
    public Guid? tenant_id { get; set; }

    [Column("created_at")]
    public DateTime created_at { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? updated_at { get; set; }

    // Removed mapped fields that don't exist:
    [NotMapped]
    public string? phone_number { get; set; }

    [NotMapped]
    public string? avatar_id { get; set; }

    [NotMapped]
    public bool is_active { get; set; } = true;
}
