using ECommerce.Service.IServices;
using ECommerce.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Service;

public static class ModuleServiceDependencies
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
    {
        services.AddTransient<IGovernoratesServices, GovernoratesServices>();

        return services;
    }

}
