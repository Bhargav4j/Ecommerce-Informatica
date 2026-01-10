using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Interfaces.Repositories;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register DbContext
        services.AddDbContext<EcommerceDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
                sqlOptions.CommandTimeout(60);
            });

            // Enable sensitive data logging in development
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (environment == "Development")
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }
        });

        // Register repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        // services.AddScoped<ICustomerRepository, CustomerRepository>();
        // services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        // services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        // services.AddScoped<IShipperRepository, ShipperRepository>();

        return services;
    }

    // Commented out - InMemory database setup should be in test projects
    // public static IServiceCollection AddInfrastructureWithInMemoryDatabase(
    //     this IServiceCollection services)
    // {
    //     // Register in-memory DbContext for testing
    //     services.AddDbContext<EcommerceDbContext>(options =>
    //     {
    //         options.UseInMemoryDatabase("EcommerceTestDb");
    //         options.EnableSensitiveDataLogging();
    //         options.EnableDetailedErrors();
    //     });

    //     // Register repositories
    //     services.AddScoped<IProductRepository, ProductRepository>();
    //     services.AddScoped<ICategoryRepository, CategoryRepository>();
    //     services.AddScoped<IBrandRepository, BrandRepository>();
    //     services.AddScoped<ISupplierRepository, SupplierRepository>();
    //     // services.AddScoped<ICustomerRepository, CustomerRepository>();
    //     // services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    //     services.AddScoped<IPersonRepository, PersonRepository>();
    //     // services.AddScoped<IInvoiceRepository, InvoiceRepository>();
    //     // services.AddScoped<IShipperRepository, ShipperRepository>();

    //     return services;
    // }
}
