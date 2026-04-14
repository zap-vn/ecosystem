using MediatR;
using ZAP.Ecosystem.Application.CRM.Features.Collections.v1.DTOs;

namespace ZAP.Ecosystem.Application.CRM.Features.Collections.v1.Queries;

public class GetCollectionListQuery : IRequest<object>
{
    public CollectionListRequestDto Request { get; set; } = new();
}
