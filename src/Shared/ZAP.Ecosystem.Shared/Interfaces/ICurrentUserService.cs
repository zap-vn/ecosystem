namespace ZAP.Ecosystem.Shared.Interfaces;
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? TenantId { get; }
        string? UserGuid { get; }
        int LocaleId { get; }
    }


