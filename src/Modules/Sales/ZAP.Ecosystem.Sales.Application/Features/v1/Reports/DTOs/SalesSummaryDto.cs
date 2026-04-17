using Newtonsoft.Json;

namespace ZAP.Ecosystem.Sales.Application.Features.Sales.v1.Reports.DTOs;
    public class SalesSummaryDto
    {
        [JsonProperty("OrderQuantity")]
        public int? OrderQuantity { get; set; }

        [JsonProperty("Cover")]
        public decimal? Cover { get; set; }

        [JsonProperty("OrderAmount")]
        public decimal? OrderAmount { get; set; }

        [JsonProperty("ServiceCharge")]
        public decimal? ServiceCharge { get; set; }

        [JsonProperty("Comp")]
        public decimal? Comp { get; set; }

        [JsonProperty("OrderDiscount")]
        public decimal? OrderDiscount { get; set; }

        [JsonProperty("OrderDiscountComp")]
        public decimal? OrderDiscountComp { get; set; }

        [JsonProperty("GrossSale")]
        public decimal? GrossSale { get; set; }

        [JsonProperty("ServiceFee")]
        public decimal? ServiceFee { get; set; }

        [JsonProperty("SurchargeFee")]
        public decimal? SurchargeFee { get; set; }

        [JsonProperty("TaxFee")]
        public decimal? TaxFee { get; set; }

        [JsonProperty("ShippingFee")]
        public decimal? ShippingFee { get; set; }

        [JsonProperty("NetSales")]
        public decimal? NetSales { get; set; }

        [JsonProperty("TotalSales")]
        public decimal? TotalSales { get; set; }

        [JsonProperty("QuantityRefund")]
        public decimal? QuantityRefund { get; set; }

        [JsonProperty("OrderRefund")]
        public decimal? OrderRefund { get; set; }

        [JsonProperty("QuantityBillCount")]
        public decimal? QuantityBillCount { get; set; }

        [JsonProperty("BillCount")]
        public decimal? BillCount { get; set; }

        [JsonProperty("BillAverage")]
        public decimal? BillAverage { get; set; }

        [JsonProperty("BillCountGrossSale")]
        public decimal? BillCountGrossSale { get; set; }

        [JsonProperty("BillAverageGrossSale")]
        public decimal? BillAverageGrossSale { get; set; }

        [JsonProperty("BillCountNetSale")]
        public decimal? BillCountNetSale { get; set; }

        [JsonProperty("BillAverageNetSale")]
        public decimal? BillAverageNetSale { get; set; }
    }




