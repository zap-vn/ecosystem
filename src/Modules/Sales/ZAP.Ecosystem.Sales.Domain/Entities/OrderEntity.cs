using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZAP.Ecosystem.Shared.Entities;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZAP.Ecosystem.Sales.Domain.Entities;
    [Table("order_header", Schema = "commerce")]
    public class OrderEntity : BaseEntity
    {
        // --- Suppress BaseEntity PascalCase properties (not in DB and avoid JSON conflict) ---
        [NotMapped, JsonIgnore] public override string? UserGuid { get; set; }
        [NotMapped, JsonIgnore] public new DateTime CreatedAt { get; set; }
        [NotMapped, JsonIgnore] public new DateTime? UpdatedAt { get; set; }
        [NotMapped, JsonIgnore] public new bool IsDeleted { get; set; }
        [NotMapped, JsonIgnore] public new Guid Id { get; set; }

        public Guid id { get; set; }
        public int serial_id { get; set; }
        public Guid? tenant_id { get; set; }
        
        [Column("order_number")]
        public string order_number { get; set; } = string.Empty;
        
        [Column("customer_id")]
        public Guid? customer_id { get; set; }
        
        [Column("location_id")]
        public Guid? location_id { get; set; }
        
        [Column("total_amount")]
        public decimal total_amount { get; set; }
        
        [Column("status_id")]
        public int status_id { get; set; }
        
        public DateTime created_at { get; set; } = DateTime.UtcNow;

        [NotMapped] public string OrderCode { get => order_number; set => order_number = value; }
        [NotMapped] public string Status => status_id.ToString();
        
        [NotMapped]
        public List<OrderItemSnapshot> Items { get; set; } = new List<OrderItemSnapshot>();
    }

    public class OrderItemSnapshot
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }




