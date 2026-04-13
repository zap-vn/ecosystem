using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZAP.Identity.Domain.Entities;

[Table("loyalty_level", Schema = "identity")]
public class LoyaltyLevel
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("level_name")]
    [MaxLength(50)]
    public string LevelName { get; set; } = string.Empty; // "Bronze", "Silver", "Gold"

    [Column("min_points")]
    public int MinPoints { get; set; }
}
