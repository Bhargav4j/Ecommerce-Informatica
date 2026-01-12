using EcommerceInformatica.Application.Interfaces.Services;
using EcommerceInformatica.Application.Mappings;
using EcommerceInformatica.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EcommerceInformatica.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register AutoMapper
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        // Register application services
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IInvoiceDetailService, InvoiceDetailService>();
        services.AddScoped<IPaymentMethodService, PaymentMethodService>();
        services.AddScoped<IProvinceService, ProvinceService>();
        services.AddScoped<ICityService, CityService>();

        return services;
    }
}
