using MediatR;
using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.Infrastructure.Data;
using ZAP.Identity.Domain.Entities;

namespace ZAP.Ecosystem.Application.App.Features.Customers.Profile.v1.Queries;

public class GetCustomerProfileQueryHandler : IRequestHandler<GetCustomerProfileQuery, CustomerProfileDto>
{
    private readonly EcosystemDbContext _context;

    public GetCustomerProfileQueryHandler(EcosystemDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerProfileDto> Handle(GetCustomerProfileQuery request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .Include(c => c.Membership)
                .ThenInclude(m => m!.LoyaltyLevel)
            .FirstOrDefaultAsync(c => c.Id == request.CustomerId && c.IsActive, cancellationToken);
        
        if (customer == null)
        {
            throw new Exception("Customer profile not found or inactive.");
        }

        return new CustomerProfileDto
        {
            Id = customer.Id,
            Username = customer.Username,
            DialingCode = customer.DialingCode,
            PhoneNumber = customer.PhoneNumber,
            Email = customer.Email,
            IsActive = customer.IsActive,
            CreatedAt = customer.CreatedAt,
            SubscriptionLevel = customer.Membership?.LoyaltyLevel?.LevelName ?? "Free Tier",
            LoyaltyPoints = customer.Membership?.CurrentPoints ?? 0,
            MembershipJoinedAt = customer.Membership?.JoinedAt,
            LastLoginAt = DateTime.UtcNow
        };
    }
}
