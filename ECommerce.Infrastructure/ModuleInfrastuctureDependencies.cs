using ECommerce.Core.IRepositories.IBasRepository;
using ECommerce.Infrastructure.Repositories.BasRepository;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure;

public static class ModuleInfrastuctureDependencies
{
    public static IServiceCollection AddInfrastuctureDependencies(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepositoryApp<>), typeof(RepositoryApp<>));
        return services;
    }

}
