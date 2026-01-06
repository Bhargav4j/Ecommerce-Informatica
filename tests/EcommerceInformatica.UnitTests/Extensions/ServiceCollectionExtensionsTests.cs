using Xunit;
using Microsoft.Extensions.DependencyInjection;
using EcommerceInformatica.Application.Extensions;
using EcommerceInformatica.Domain.Interfaces.Services;
using EcommerceInformatica.Application.Services;
using AutoMapper;

namespace EcommerceInformatica.UnitTests.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddApplicationServices_ShouldRegisterAutoMapper()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var mapper = serviceProvider.GetService<IMapper>();
        Assert.NotNull(mapper);
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterCategoryService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var serviceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(ICategoryService));
        Assert.NotNull(serviceDescriptor);
        Assert.Equal(ServiceLifetime.Scoped, serviceDescriptor.Lifetime);
        Assert.Equal(typeof(CategoryService), serviceDescriptor.ImplementationType);
    }

    [Fact]
    public void AddApplicationServices_ShouldRegisterProductService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationServices();
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var serviceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(IProductService));
        Assert.NotNull(serviceDescriptor);
        Assert.Equal(ServiceLifetime.Scoped, serviceDescriptor.Lifetime);
        Assert.Equal(typeof(ProductService), serviceDescriptor.ImplementationType);
    }

    [Fact]
    public void AddApplicationServices_ShouldReturnServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddApplicationServices();

        // Assert
        Assert.Same(services, result);
    }
}
