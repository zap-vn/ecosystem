using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Collections.v1.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ZAP.Ecosystem.Application.CRM.Features.Collections.v1.Queries;

public class GetCollectionListQueryHandler : IRequestHandler<GetCollectionListQuery, object>
{
    private readonly ICollectionRepository _repository;

    public GetCollectionListQueryHandler(ICollectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<object> Handle(GetCollectionListQuery request, CancellationToken cancellationToken)
    {
        var req = request.Request;
        var (items, total) = await _repository.GetPagedAsync(req.page_index, req.page_size, req.search);

        var dtos = items.Select(c => new CollectionDto
        {
            id            = c.id,
            name          = c.name,
            image_url     = c.banner_url,
            product_count = c.items.Count,
            status_id     = c.status_id,
        }).ToList();

        return CrmResponse.Paged(new PagedResult<CollectionDto>(dtos, total, req.page_index, req.page_size));
    }
}
