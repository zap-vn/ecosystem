using System.Collections.Generic;

namespace ZAP.Identity.Application.Common.Models;
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        
        public static ApiResponse<T> SuccessResult(T data, string message = "Success") => 
            new ApiResponse<T> { Success = true, Data = data, Message = message };

        public static ApiResponse<T> ErrorResult(string message, List<string>? errors = null) => 
            new ApiResponse<T> { Success = false, Message = message, Errors = errors };
    }

    public class ApiResponse : ApiResponse<object>
    {
        public static ApiResponse SuccessResult(string message = "Success") => 
            new ApiResponse { Success = true, Message = message };

        public new static ApiResponse ErrorResult(string message, List<string>? errors = null) => 
            new ApiResponse { Success = false, Message = message, Errors = errors };
    }


