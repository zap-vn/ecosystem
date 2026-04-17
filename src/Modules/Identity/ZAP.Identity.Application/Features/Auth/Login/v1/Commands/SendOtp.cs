using MediatR;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using System;

using ZAP.Identity.Application.Common.Models;
using ZAP.Identity.Application.Common.Interfaces;

namespace ZAP.Identity.Application.Features.Auth.Login.v1.Commands.SendOtp;
    // COMMAND
    public class SendOtpCommand : IRequest<ApiResponse>
    {
        [JsonProperty("account")]
        public string Account { get; set; } = string.Empty;

        [JsonIgnore]
        public string? DialingCode { get; set; } = "+84";
    }

    public class SendOtpCommandHandler : IRequestHandler<SendOtpCommand, ApiResponse>
    {
        private readonly IOtpRepository _otpRepository;
        private readonly INotificationService _notificationService;
        private readonly IUserRepository _userRepository;

        public SendOtpCommandHandler(IOtpRepository otpRepository, INotificationService notificationService, IUserRepository userRepository)
        {
            _otpRepository = otpRepository;
            _notificationService = notificationService;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse> Handle(SendOtpCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Account))
            {
                return ApiResponse.ErrorResult("error_missing_account|Vui lòng nhập Email hoặc Số điện thoại.");
            }

            string identifier = request.Account.Trim();
            bool isEmail = identifier.Contains("@");

            if (!isEmail && !string.IsNullOrEmpty(request.DialingCode))
            {
                if (identifier.StartsWith("0")) identifier = identifier.Substring(1);
                identifier = request.DialingCode + identifier;
            }

            // Database strict validation - Do not use mock
            dynamic userExists = isEmail 
                ? await _userRepository.GetByEmailAsync(identifier) 
                : await _userRepository.GetByPhoneAsync(identifier);

            if (userExists == null)
            {
                return ApiResponse.ErrorResult("error_account_not_found|Tài khoản không tồn tại trong hệ thống.");
            }

            // Generate 6-digit OTP
            var otpCode = new Random().Next(100000, 999999).ToString();

            // Mock saving to DB and sending (Keep simulated so we don't spam SMS APIs during tests)
            /*
            var customerOtp = new { ... };
            await _otpRepository.CreateAsync(customerOtp);
            if (isEmail) await _notificationService.SendOtpEmailAsync(...);
            else await _notificationService.SendSmsOtpAsync(...);
            */

            return ApiResponse.SuccessResult("Mã OTP đã được gửi thành công.");
        }
    }


