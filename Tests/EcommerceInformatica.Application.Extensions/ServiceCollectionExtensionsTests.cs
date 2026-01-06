using Xunit;
using Microsoft.Extensions.DependencyInjection;
using EcommerceInformatica.Application.Extensions;
using AutoMapper;

namespace EcommerceInformatica.Application.Extensions.Tests;

/// <summary>
/// Test class for ServiceCollectionExtensions
/// </summary>
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
    public void AddApplicationServices_ShouldReturnServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddApplicationServices();

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IServiceCollection>(result);
    }

    [Fact]
    public void AddApplicationServices_AutoMapperConfiguration_ShouldBeValid()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddApplicationServices();
        var serviceProvider = services.BuildServiceProvider();

        // Act
        var mapper = serviceProvider.GetService<IMapper>();

        // Assert
        Assert.NotNull(mapper);
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    [Fact]
    public void AddApplicationServices_ShouldAllowChaining()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddApplicationServices()
                            .AddSingleton<string>("test");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, services.Count(s => s.Lifetime == ServiceLifetime.Singleton));
    }
}
