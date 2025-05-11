using Tutorial8.Services;
using Tutorial8.Services.Core;

namespace Tutorial8.Dependencies;

public static class Dependencies
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        // Add your dependencies here
        services.AddScoped<IWarehouseService, WarehouseService>();

        return services;
    }
}
