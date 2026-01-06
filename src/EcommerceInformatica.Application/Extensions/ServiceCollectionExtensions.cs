using EcommerceInformatica.Application.Services;
using EcommerceInformatica.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceInformatica.Application.Extensions;

/// <summary>
/// Extension methods for service registration in the Application layer
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register AutoMapper
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

        // Register services
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
