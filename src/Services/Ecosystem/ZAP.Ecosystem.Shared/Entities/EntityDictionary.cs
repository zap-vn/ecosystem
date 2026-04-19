using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ZAP.Ecosystem.Shared.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZAP.Ecosystem.Shared.Entities;

[Table("entity_dictionary", Schema = "system")]
public class EntityDictionary : BaseEntity
{
    [Column("serial_id")]
    public int SerialId { get; set; }

    [Column("serial_number")]
    public string? SerialNumber { get; set; }

    [Column("tenant_id")]
    public Guid? TenantId { get; set; }

    [Column("schema_name")]
    public string SchemaName { get; set; } = string.Empty;

    [Column("table_name")]
    public string TableName { get; set; } = string.Empty;

    [Column("display_name")]
    public string DisplayName { get; set; } = string.Empty;

    [Column("description")]
    public string? Description { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    public virtual ICollection<EntityFieldDictionary> Fields { get; set; } = new List<EntityFieldDictionary>();
}
