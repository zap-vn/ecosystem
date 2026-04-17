using Newtonsoft.Json;

namespace ZAP.Ecosystem.Shared.Responses;

public class ApiResponse<T>
{
    public string Message { get; set; } = string.Empty;

    public bool Success { get; set; }

    public int StatusCode { get; set; }

    public string? ErrorCode { get; set; }

    public T? Data { get; set; }

    public static ApiResponse<T> Ok(T data, string message = "Success")
    {
        return new ApiResponse<T>
        {
            Success = true,
            StatusCode = 200,
            Message = message,
            ErrorCode = null,
            Data = data
        };
    }

    public static ApiResponse<T> Failure(int statusCode, string message, string? errorCode = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = statusCode,
            Message = message,
            ErrorCode = errorCode,
            Data = default
        };
    }
}


