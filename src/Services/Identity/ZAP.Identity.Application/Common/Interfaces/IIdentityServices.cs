using System.Threading.Tasks;

// Cấu trúc Interface chuẩn hoá 
namespace ZAP.Identity.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<dynamic> GetByEmailAsync(string email);
        Task<dynamic> GetByPhoneAsync(string phone);
    }

    public interface IOtpRepository
    {
        Task CreateAsync(dynamic customerOtp);
    }

    public interface ITokenGenerator
    {
        // Skill 04: Cần sinh token chứa tenant_id cho portal CRM
        Task<string> GenerateTokenAsync(dynamic user);
    }
    
    public interface INotificationService
    {
        Task SendOtpEmailAsync(string email, string otpCode, string name);
        Task SendSmsOtpAsync(string phone, string otpCode);
    }
}
