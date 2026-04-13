using MediatR;

namespace ZAP.Identity.Application.Features.Customers.Profile.V1.Queries;

public class CustomerProfileDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string DialingCode { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Thuộc tính mô phỏng Join thêm dữ liệu từ bảng khác
    public string SubscriptionLevel { get; set; } = "Free";
    public int LoyaltyPoints { get; set; } = 0;
    public DateTime? MembershipJoinedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
}

public class GetCustomerProfileQuery : IRequest<CustomerProfileDto>
{
    public Guid CustomerId { get; set; }
}
