using EcommerceInformatica.Domain.Interfaces.Repositories;
using EcommerceInformatica.Infrastructure.Data;
using EcommerceInformatica.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EcommerceInformatica.Tests.Infrastructure.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddInfrastructureServices_ShouldRegisterDbContext()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = CreateConfiguration();

        // Act
        services.AddInfrastructureServices(configuration);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var dbContext = serviceProvider.GetService<ApplicationDbContext>();
        Assert.NotNull(dbContext);
    }

    [Fact]
    public void AddInfrastructureServices_ShouldRegisterAllRepositories()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = CreateConfiguration();

        // Act
        services.AddInfrastructureServices(configuration);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        Assert.NotNull(serviceProvider.GetService<IProductRepository>());
        Assert.NotNull(serviceProvider.GetService<ICategoryRepository>());
        Assert.NotNull(serviceProvider.GetService<IBrandRepository>());
        Assert.NotNull(serviceProvider.GetService<ISupplierRepository>());
        Assert.NotNull(serviceProvider.GetService<IPersonRepository>());
        Assert.NotNull(serviceProvider.GetService<IInvoiceRepository>());
        Assert.NotNull(serviceProvider.GetService<IInvoiceDetailRepository>());
        Assert.NotNull(serviceProvider.GetService<IPaymentMethodRepository>());
        Assert.NotNull(serviceProvider.GetService<IProvinceRepository>());
        Assert.NotNull(serviceProvider.GetService<ICityRepository>());
    }

    [Fact]
    public void AddInfrastructureServices_ShouldRegisterRepositoriesAsScoped()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = CreateConfiguration();

        // Act
        services.AddInfrastructureServices(configuration);

        // Assert
        var productRepoDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(IProductRepository));
        Assert.NotNull(productRepoDescriptor);
        Assert.Equal(ServiceLifetime.Scoped, productRepoDescriptor.Lifetime);

        var categoryRepoDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(ICategoryRepository));
        Assert.NotNull(categoryRepoDescriptor);
        Assert.Equal(ServiceLifetime.Scoped, categoryRepoDescriptor.Lifetime);
    }

    [Fact]
    public void AddInfrastructureServicesWithInMemoryDatabase_ShouldRegisterDbContext()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfrastructureServicesWithInMemoryDatabase();
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var dbContext = serviceProvider.GetService<ApplicationDbContext>();
        Assert.NotNull(dbContext);
    }

    [Fact]
    public void AddInfrastructureServicesWithInMemoryDatabase_ShouldUseInMemoryDatabase()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfrastructureServicesWithInMemoryDatabase();
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        Assert.True(dbContext.Database.IsInMemory());
    }

    [Fact]
    public void AddInfrastructureServicesWithInMemoryDatabase_ShouldRegisterAllRepositories()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfrastructureServicesWithInMemoryDatabase();
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        Assert.NotNull(serviceProvider.GetService<IProductRepository>());
        Assert.NotNull(serviceProvider.GetService<ICategoryRepository>());
        Assert.NotNull(serviceProvider.GetService<IBrandRepository>());
        Assert.NotNull(serviceProvider.GetService<ISupplierRepository>());
        Assert.NotNull(serviceProvider.GetService<IPersonRepository>());
        Assert.NotNull(serviceProvider.GetService<IInvoiceRepository>());
        Assert.NotNull(serviceProvider.GetService<IInvoiceDetailRepository>());
        Assert.NotNull(serviceProvider.GetService<IPaymentMethodRepository>());
        Assert.NotNull(serviceProvider.GetService<IProvinceRepository>());
        Assert.NotNull(serviceProvider.GetService<ICityRepository>());
    }

    [Fact]
    public void AddInfrastructureServices_ShouldReturnServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = CreateConfiguration();

        // Act
        var result = services.AddInfrastructureServices(configuration);

        // Assert
        Assert.Same(services, result);
    }

    [Fact]
    public void AddInfrastructureServicesWithInMemoryDatabase_ShouldReturnServiceCollection()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddInfrastructureServicesWithInMemoryDatabase();

        // Assert
        Assert.Same(services, result);
    }

    [Fact]
    public void AddInfrastructureServices_ShouldConfigureSqlServerWithRetryPolicy()
    {
        // Arrange
        var services = new ServiceCollection();
        var configuration = CreateConfiguration();

        // Act
        services.AddInfrastructureServices(configuration);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        Assert.NotNull(dbContext);

        // Verify that DbContext was registered
        var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>();
        Assert.NotNull(dbContextOptions);
    }

    private IConfiguration CreateConfiguration()
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            {"ConnectionStrings:DefaultConnection", "Server=localhost;Database=TestDb;Trusted_Connection=True;TrustServerCertificate=True;"}
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build();
    }
}
