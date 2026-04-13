using System.Text.Json.Serialization;

namespace ZAP.Ecosystem.Shared.Responses;

public class ApiResponse<T>
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("status_code")]
    public int StatusCode { get; set; }

    [JsonPropertyName("error_code")]
    public string? ErrorCode { get; set; }

    [JsonPropertyName("data")]
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
