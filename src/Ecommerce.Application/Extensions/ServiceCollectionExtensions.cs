using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Application.Extensions;

/// <summary>
/// Extension methods for registering Application services
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

        return services;
    }
}
