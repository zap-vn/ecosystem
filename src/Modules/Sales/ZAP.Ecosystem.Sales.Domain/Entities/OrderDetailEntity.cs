using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZAP.Ecosystem.Shared.Entities;
using System.Collections.Generic;

namespace ZAP.Ecosystem.Sales.Domain.Entities;
    [Table("order_detail", Schema = "commerce")]
    [PrimaryKey(nameof(Id))]
    public class OrderDetailEntity
    {
                public string Id { get; set; } = string.Empty;
                public string OrderId { get; set; } = string.Empty;

                [NotMapped]
                public OrderSummaryInfo OrderSummary { get; set; } = new OrderSummaryInfo();

                [NotMapped]
                public object? Shipping { get; set; }

                [NotMapped]
                public List<object>? PaymentList { get; set; }
    }

        public class OrderSummaryInfo
    {
                public decimal Subtotal { get; set; }

                public decimal Discount { get; set; }

                public decimal Grandtotal { get; set; }

                public decimal ShippingValue { get; set; }
    }







