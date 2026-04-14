using ZAP.Ecosystem.Shared.Data;

namespace ZAP.Ecosystem.Application.CRM.Common;

public static class CrmResponse
{
    public static object Ok(object? data, string message = "OK") =>
        new { success = true, code = 200, message, data };

    public static object Paged<T>(PagedResult<T> result, string message = "OK") =>
        new
        {
            success = true,
            code = 200,
            message,
            data = new
            {
                total_page = result.PageSize > 0 ? (int)Math.Ceiling((double)result.TotalCount / result.PageSize) : 1,
                total_record = result.TotalCount,
                page_index = result.PageIndex,
                page_size = result.PageSize,
                items = result.Items
            }
        };

    public static object Created(object? data, string message = "Created successfully") =>
        new { success = true, code = 200, message, data };

    public static object Updated(object? data, string message = "Updated successfully") =>
        new { success = true, code = 200, message, data };

    public static object NotFound(string resource = "Resource") =>
        new { success = false, code = 404, message = $"{resource} not found", data = (object?)null };
}
