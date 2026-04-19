using MediatR;

namespace ZAP.CRM.Catalog.Application.Features.Commons.Units;

public class CreateUnitCommand : IRequest<object>
{
    public string code { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public int precision { get; set; }
    public bool is_active { get; set; } = true;
    public int? group_id { get; set; }
}

public class UpdateUnitCommand : CreateUnitCommand
{
    public int id { get; set; }
}

public class DeleteUnitCommand(int id) : IRequest<object>
{
    public int Id { get; } = id;
}
