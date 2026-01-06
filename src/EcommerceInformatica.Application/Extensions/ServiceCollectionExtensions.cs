using Microsoft.Extensions.DependencyInjection;

namespace EcommerceInformatica.Application.Extensions;

/// <summary>
/// Extension methods for registering application layer services
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register AutoMapper
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

        // Services will be registered here

        return services;
    }
}
