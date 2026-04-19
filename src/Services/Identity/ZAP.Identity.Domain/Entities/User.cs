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

    [Column("email")]
    [MaxLength(150)]
    public string? email { get; set; }

    [Column("username")]
    [MaxLength(50)]
    public string? phone_number { get; set; }

    [Column("password_hash")]
    [MaxLength(500)]
    public string password_hash { get; set; } = string.Empty;

    [Column("full_name")]
    [MaxLength(200)]
    public string? full_name { get; set; }

    [NotMapped]
    [MaxLength(500)]
    public string? avatar_id { get; set; }

    [Column("status_id")]
    public int status_id { get; set; } = 9001;

    [Column("tenant_id")]
    public Guid? tenant_id { get; set; }

    [NotMapped]
    public bool is_active { get; set; } = true;

    [Column("created_at")]
    public DateTime created_at { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? updated_at { get; set; }
}
