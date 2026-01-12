using EcommerceInformatica.Domain.Interfaces.Repositories;
using EcommerceInformatica.Infrastructure.Data;
using EcommerceInformatica.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceInformatica.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });
        });

        // Register repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IInvoiceDetailRepository, InvoiceDetailRepository>();
        services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        services.AddScoped<IProvinceRepository, ProvinceRepository>();
        services.AddScoped<ICityRepository, CityRepository>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServicesWithInMemoryDatabase(
        this IServiceCollection services)
    {
        // Register DbContext with In-Memory database for testing
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase("EcommerceInformaticaTestDb");
        });

        // Register repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IInvoiceDetailRepository, InvoiceDetailRepository>();
        services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        services.AddScoped<IProvinceRepository, ProvinceRepository>();
        services.AddScoped<ICityRepository, CityRepository>();

        return services;
    }
}
