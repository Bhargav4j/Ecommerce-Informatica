using Microsoft.Extensions.DependencyInjection;
using Ecommerce.Application.Services;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IPersonService, PersonService>();

        return services;
    }
}
