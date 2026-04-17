using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace ZAP.CRM.Catalog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}



