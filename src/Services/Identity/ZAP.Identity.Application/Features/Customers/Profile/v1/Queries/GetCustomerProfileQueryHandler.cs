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
        
        var pagedResult = await _customerRepository.GetPagedAsync(
            pageIndex: 1,
            pageSize: 1,
            filter: c => c.Id == request.CustomerId && c.IsActive,
            cancellationToken: cancellationToken);

        var customer = pagedResult.Items.FirstOrDefault();

        if (customer == null)
        {
            throw new Exception("Customer profile not found or inactive.");
        }

        // 2. Map dữ liệu Entiy sang DTO (Bao gồm Join dữ liệu thông tin mở rộng)
        return new CustomerProfileDto
        {
            Id = customer.Id,
            Username = customer.Username,
            Email = customer.Email,
            IsActive = customer.IsActive,
            CreatedAt = customer.CreatedAt,
            // Ví dụ Join dữ liệu
            SubscriptionLevel = "Premium", // Giả lập dữ liệu Join lấy từ bảng CustomerSubscriptions
            LastLoginAt = DateTime.UtcNow  // Giả lập dữ liệu Join lấy từ bảng CustomerActivityLog
        };
    }
}
