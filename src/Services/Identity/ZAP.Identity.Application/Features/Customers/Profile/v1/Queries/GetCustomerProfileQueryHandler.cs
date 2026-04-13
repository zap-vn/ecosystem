using MediatR;
using ZAP.Ecosystem.Shared.Data;
using ZAP.Identity.Domain.Entities;

namespace ZAP.Identity.Application.Features.Customers.Profile.V1.Queries;

public class GetCustomerProfileQueryHandler : IRequestHandler<GetCustomerProfileQuery, CustomerProfileDto>
{
    private readonly IBaseRepository<Customer> _customerRepository;

    public GetCustomerProfileQueryHandler(IBaseRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerProfileDto> Handle(GetCustomerProfileQuery request, CancellationToken cancellationToken)
    {
        // 1. Dùng repository để Get theo Id
        // Trong thực tế, bạn có thể triển khai hàm _customerRepository.GetByIdAsync(...) hoặc
        // Mở rộng Generic Repository để hỗ trợ Include/Join các Relation: query.Include(x => x.Profile)
        
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        
        if (customer == null)
        {
            throw new Exception("Customer profile not found or inactive.");
        }

        // 2. Map dữ liệu Entiy sang DTO
        return new CustomerProfileDto
        {
            Id = customer.Id,
            Username = customer.Username,
            DialingCode = customer.DialingCode,
            PhoneNumber = customer.PhoneNumber,
            Email = customer.Email,
            IsActive = customer.IsActive,
            CreatedAt = customer.CreatedAt,
            // Natively extract real Join references once Eager Loading is executed
            SubscriptionLevel = customer.Membership?.LoyaltyLevel?.LevelName ?? "Free Tier",
            LoyaltyPoints = customer.Membership?.CurrentPoints ?? 0,
            MembershipJoinedAt = customer.Membership?.JoinedAt,
            LastLoginAt = DateTime.UtcNow  // Placeholder for Activity Log
        };
    }
}
