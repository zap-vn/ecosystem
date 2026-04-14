using ZAP.Ecosystem.Domain.CRM.Common;
using System.Collections.Generic;

namespace ZAP.Ecosystem.Domain.CRM
{
        public class OrderDetailEntity
    {
                        public string Id { get; set; } = string.Empty;

                public string OrderId { get; set; } = string.Empty;

                public OrderSummaryInfo OrderSummary { get; set; } = new OrderSummaryInfo();

                public object? Shipping { get; set; }

                public List<object>? PaymentList { get; set; }
    }

        public class OrderSummaryInfo
    {
                public decimal Subtotal { get; set; }

                public decimal Discount { get; set; }

                public decimal Grandtotal { get; set; }

                public decimal ShippingValue { get; set; }
    }
}

