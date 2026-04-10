using MediatR;
using System.Text.Json.Serialization;
using ZAP.Identity.Application.Features.Auth.AppAuth.v1.DTOs;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
// Bổ sung các Using Interfaces giả định theo cấu trúc chuẩn
using ZAP.Identity.Application.Common.Models;
using ZAP.Identity.Application.Common.Interfaces;

namespace ZAP.Identity.Application.Features.Auth.AppAuth.v1.Commands.CheckAccountAvailability
{
    // COMMAND
    public class CheckAccountAvailabilityCommand : IRequest<ApiResponse<CheckAccountDataDto>>
    {
        [JsonPropertyName("account")]
        public string Account { get; set; } = string.Empty;

        [JsonIgnore]
        public string? DialingCode { get; set; } = "+84";
        [JsonIgnore]
        public string Provider { get; set; } = "Email"; 
        [JsonIgnore]
        public bool IsLogin { get; set; } = true;
    }

    // HANDLER 
    // Ghi chú: Sử dụng dynamic cho các dependencies chưa được tái tạo trong Interface
    public class CheckAccountAvailabilityCommandHandler : IRequestHandler<CheckAccountAvailabilityCommand, ApiResponse<CheckAccountDataDto>>
    {
        private readonly IUserRepository _userRepository;

        public CheckAccountAvailabilityCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<CheckAccountDataDto>> Handle(CheckAccountAvailabilityCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Account))
            {
                return ApiResponse<CheckAccountDataDto>.ErrorResult("Vui lòng nhập Email hoặc Số điện thoại.");
            }

            string identifier = request.Account.Trim();
            bool isEmail = identifier.Contains("@");

            // Normalize phone number if dialing_code is present
            if (!isEmail && !string.IsNullOrEmpty(request.DialingCode))
            {
                if (identifier.StartsWith("0")) identifier = identifier.Substring(1);
                identifier = request.DialingCode + identifier;
            }

            var user = await _userRepository.GetByEmailAsync(identifier);
            if (user == null && !isEmail) user = await _userRepository.GetByPhoneAsync(identifier);
            
            bool accountExists = user != null;

            if (!accountExists)
            {
                if (request.IsLogin)
                {
                    return ApiResponse<CheckAccountDataDto>.ErrorResult("Email hoặc số điện thoại chưa được đăng ký.");
                }
                else
                {
                    return ApiResponse<CheckAccountDataDto>.SuccessResult(
                        new CheckAccountDataDto { Exists = false, Methods = new List<string> { "otp" } },
                        "Tài khoản khả dụng."
                    );
                }
            }

            var methods = new List<string> { "otp" };
            // Optional: Kiểm tra password hash
            // if (user != null && !string.IsNullOrEmpty(user.password_hash)) methods.Add("password");

            return ApiResponse<CheckAccountDataDto>.SuccessResult(
                new CheckAccountDataDto { Exists = true, Methods = methods },
                "Kiểm tra tài khoản thành công."
            );
        }
    }
}
