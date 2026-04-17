using Newtonsoft.Json;

namespace ZAP.Ecosystem.Sales.Application.Features.Sales.v1.Reports.DTOs;
    public class ReportResponseDto<T>
    {
        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("Model")]
        public T Model { get; set; } = default!;
    }

    public class OverviewResponse
    {
        [JsonProperty("Overview")]
        public SalesSummaryDto Overview { get; set; } = default!;
    }




