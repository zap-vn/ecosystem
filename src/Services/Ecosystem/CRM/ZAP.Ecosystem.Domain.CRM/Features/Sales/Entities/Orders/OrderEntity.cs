using ZAP.Ecosystem.Domain.CRM.Common;
using System;
using System.Collections.Generic;

namespace CRM.Sales.Domain.Entities.Orders
{
    public class OrderEntity : BaseEntity
    {
                public override string? UserGuid { get; set; }

        public string OrderCode { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
        
        // Product data snapshot to optimize millions of rows query
        public List<OrderItemSnapshot> Items { get; set; } = new List<OrderItemSnapshot>();
    }

    public class OrderItemSnapshot
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty; // Store product name directly (translated) at the time of purchase
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
