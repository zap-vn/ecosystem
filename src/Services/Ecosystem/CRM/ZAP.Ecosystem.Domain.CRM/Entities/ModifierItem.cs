using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZAP.Ecosystem.Domain.CRM
{
    [Table("modifier_item", Schema = "catalog")]
    public class ModifierItem
    {
        [Key]
        public Guid id { get; set; } = Guid.NewGuid();

        [Column("serial_id")]
        public int? serial_id { get; set; }

        [Column("serial_number")]
        public string? serial_number { get; set; }

        [Column("group_id")]
        public Guid? group_id { get; set; }

        [Column("product_variant_id")]
        public Guid? product_variant_id { get; set; }

        [Column("image_url")]
        public string? image_url { get; set; }

        [Column("price_override")]
        public decimal? price_override { get; set; }

        [Column("sort_order")]
        public int sort_order { get; set; } = 0;

        [Column("status_id")]
        public int? status_id { get; set; }

        [Column("created_at")]
        public DateTime created_at { get; set; }

        [Column("updated_at")]
        public DateTime? updated_at { get; set; }

        [NotMapped]
        public string? status_code { get; set; }

        [NotMapped]
        public string? status_name { get; set; }
    }
}
