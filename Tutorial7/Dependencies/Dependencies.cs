using Tutorial7.Services;
using Tutorial7.Services.Core;

namespace Tutorial7.Dependencies;

public static class Dependencies
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IClientService, ClientService>();
        services.AddTransient<ITripService, TripService>();
        return services;
    }
}
