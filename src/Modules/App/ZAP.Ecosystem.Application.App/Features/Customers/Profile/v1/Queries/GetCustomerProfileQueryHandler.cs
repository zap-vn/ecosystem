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
        var customer = await _context.CrmCustomers
            .Include(c => c.loyalty_tier)
            .FirstOrDefaultAsync(c => c.id == request.CustomerId, cancellationToken);
        
        if (customer == null)
        {
            throw new Exception("Customer profile not found or inactive.");
        }

        return new CustomerProfileDto
        {
            Id = customer.id,
            Username = customer.phone_number ?? string.Empty,
            DialingCode = "",
            PhoneNumber = customer.phone_number ?? string.Empty,
            Email = customer.email ?? string.Empty,
            IsActive = customer.status_id == 1,
            CreatedAt = customer.created_at,
            SubscriptionLevel = customer.loyalty_tier?.tier_name ?? "Free Tier",
            LoyaltyPoints = (int)customer.current_points_balance,
            MembershipJoinedAt = customer.created_at,
            LastLoginAt = DateTime.UtcNow
        };
    }
}


