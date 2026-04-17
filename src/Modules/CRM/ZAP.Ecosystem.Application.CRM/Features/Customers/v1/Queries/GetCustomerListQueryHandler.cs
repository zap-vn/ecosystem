using MediatR;


namespace ZAP.Ecosystem.CRM.Application.Features.Customers.v1.Queries;

public class GetCustomerListQueryHandler : IRequestHandler<GetCustomerListQuery, object>
{
    private readonly ICustomerRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public GetCustomerListQueryHandler(ICustomerRepository repository, ICurrentUserService currentUserService)
    {
        _repository = repository;
        _currentUserService = currentUserService;
    }

    public async Task<object> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
    {
        var req = request.Request;
        Guid? tenantId = Guid.TryParse(_currentUserService.UserGuid, out var g) ? g : null;

        var pagedResult = await _repository.GetPagedAsync(
            req.PageIndex,
            req.PageSize,
            tenantId,
            req.Search,
            req.Filters?.StatusId,
            req.Filters?.TierId,
            req.Filters?.MinTotalSpent,
            req.Filters?.MaxTotalSpent,
            req.Filters?.MinPoints,
            req.Filters?.MaxPoints,
            req.Sort?.Field ?? "full_name",
            req.Sort?.Descending ?? false);

        var dtos = pagedResult.Items.Select(c => new CustomerListDto
        {
            id = c.id,
            serial_id = c.serial_id,
            full_name = c.full_name ?? string.Empty,
            phone_number = c.phone_number,
            email = c.email,
            current_points_balance = c.current_points_balance,
            total_spent_amount = c.total_spent_amount,
            tier_id = c.tier_id,
            tier_name = c.loyalty_tier?.tier_name,
            status_id = c.status_id ?? 0,
            status_code = c.status?.code,
            status_name = c.status?.translations?.FirstOrDefault(t => t.locale_id == _currentUserService.LocaleId)?.name
                       ?? c.status?.translations?.FirstOrDefault(t => t.locale_id == 2)?.name
        }).ToList();

        return CrmResponse.Paged(new PagedResult<CustomerListDto>(dtos, pagedResult.TotalCount, pagedResult.PageIndex, pagedResult.PageSize));
    }
}




