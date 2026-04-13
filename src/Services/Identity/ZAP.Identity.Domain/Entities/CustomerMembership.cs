using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZAP.Identity.Domain.Entities;

[Table("customer_membership", Schema = "identity")]
public class CustomerMembership
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("customer_id")]
    public Guid CustomerId { get; set; }
    
    [Column("loyalty_level_id")]
    public Guid? LoyaltyLevelId { get; set; }
    
    [Column("current_points")]
    public int CurrentPoints { get; set; }
    
    [Column("joined_at")]
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; } = null!;

    [ForeignKey(nameof(LoyaltyLevelId))]
    public LoyaltyLevel? LoyaltyLevel { get; set; } = null;
}
