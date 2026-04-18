using MediatR;
using System;

namespace ZAP.CRM.Catalog.Application.Features.Commons.Units;

public class GetUnitByIdQuery(int id) : IRequest<object>
{
    public int Id { get; } = id;
}
