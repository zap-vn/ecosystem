using MediatR;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System;

using ZAP.Identity.Application.Common.Models;
using ZAP.Identity.Application.Common.Interfaces;

namespace ZAP.Identity.Application.Features.Auth.Login.v1.Commands.SendOtp
{
    // COMMAND
    public class SendOtpCommand : IRequest<ApiResponse>
    {
        [JsonPropertyName("account")]
        public string Account { get; set; } = string.Empty;

        [JsonIgnore]
        public string? DialingCode { get; set; } = "+84";
    }

    public class SendOtpCommandHandler : IRequestHandler<SendOtpCommand, ApiResponse>
    {
        private readonly IOtpRepository _otpRepository;
        private readonly INotificationService _notificationService;

        public SendOtpCommandHandler(IOtpRepository otpRepository, INotificationService notificationService)
        {
            _otpRepository = otpRepository;
            _notificationService = notificationService;
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

            // Generate 6-digit OTP
            var otpCode = new Random().Next(100000, 999999).ToString();

            // Mock saving to DB and sending
            /*
            var customerOtp = new { ... };
            await _otpRepository.CreateAsync(customerOtp);
            if (isEmail) await _notificationService.SendOtpEmailAsync(...);
            else await _notificationService.SendSmsOtpAsync(...);
            */

            return ApiResponse.SuccessResult("Mã OTP đã được gửi thành công.");
        }
    }
}
