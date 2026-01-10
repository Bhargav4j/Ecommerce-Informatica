using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Mappings;
using Ecommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register AutoMapper
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        // Register application services
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<IPersonService, PersonService>();
        // TODO: Implement these services when needed
        // services.AddScoped<ICustomerService, CustomerService>();
        // services.AddScoped<IEmployeeService, EmployeeService>();
        // services.AddScoped<IInvoiceService, InvoiceService>();
        // services.AddScoped<IShipperService, ShipperService>();

        return services;
    }
}
