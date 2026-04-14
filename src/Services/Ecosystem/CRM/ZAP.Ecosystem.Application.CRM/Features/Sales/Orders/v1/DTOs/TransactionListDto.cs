using System;

namespace ZAP.Ecosystem.Application.CRM.Features.Orders.v1.DTOs
{
    public class TransactionListDto
    {
        public Guid id { get; set; }
        public string order_number { get; set; } = string.Empty;
        public decimal total_amount { get; set; }
        public string status_text { get; set; } = string.Empty;
        public DateTime created_at { get; set; }
        public string customer_name { get; set; } = "Guest";
    }
}

