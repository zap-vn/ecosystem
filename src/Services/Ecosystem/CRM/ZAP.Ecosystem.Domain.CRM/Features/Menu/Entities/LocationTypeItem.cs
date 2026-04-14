using ZAP.Ecosystem.Domain.CRM.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Menu.Domain.Entities
{
    [Table("lookups", Schema = "system")]
    public class LocationTypeItem
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("code")]
        public string? code { get; set; }

        public virtual System.Collections.Generic.ICollection<LocationTypeTranslation> translations { get; set; } = new System.Collections.Generic.List<LocationTypeTranslation>();
    }
}

