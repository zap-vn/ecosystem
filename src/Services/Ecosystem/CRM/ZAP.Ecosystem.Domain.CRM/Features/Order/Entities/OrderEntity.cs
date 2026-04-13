using ZAP.Ecosystem.Domain.CRM.Common;
using System;
using System.Collections.Generic;

namespace CRM.Order.Domain.Entities
{
    public class OrderEntity : BaseEntity
    {
                public long Key { get; set; } 

                public string OrderCode { get; set; } = string.Empty;

                public string? CartId { get; set; }

                public int DiningOptionId { get; set; }

                public string? CustomerGuestGuid { get; set; }

                public string? LocationGuid { get; set; }

                public string? DeviceGuid { get; set; }

                public string? AssignToLocationGuid { get; set; }

                public decimal TotalAmount { get; set; }

                public int OrderStatusId { get; set; }

                public int PaymentStatusId { get; set; }

        public string Status => OrderStatusId.ToString();
        
        public List<OrderItemSnapshot> Items { get; set; } = new List<OrderItemSnapshot>();
    }

    public class OrderItemSnapshot
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
