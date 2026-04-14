namespace ZAP.Ecosystem.Application.CRM.Common.Interfaces;

public interface ICurrentUserService
{
    string? UserGuid { get; }
    int LocaleId { get; }
}
