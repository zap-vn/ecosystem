using System;

namespace ZAP.Ecosystem.Shared.Data;
    public class CrmPagedRequest<T>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public T? Filters { get; set; }
        public SortOptions? Sort { get; set; }
    }

    public class SortOptions
    {
        public string? Field { get; set; }
        public bool Descending { get; set; }
    }


