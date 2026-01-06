using Xunit;
using Microsoft.EntityFrameworkCore;
using EcommerceInformatica.Infrastructure.Data;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Infrastructure.Data.Tests;

/// <summary>
/// Test class for EcommerceDbContext
/// </summary>
public class EcommerceDbContextTests
{
    private DbContextOptions<EcommerceDbContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void EcommerceDbContext_Constructor_ShouldInitialize()
    {
        // Arrange
        var options = CreateNewContextOptions();

        // Act
        using var context = new EcommerceDbContext(options);

        // Assert
        Assert.NotNull(context);
        Assert.NotNull(context.Products);
        Assert.NotNull(context.Categories);
        Assert.NotNull(context.Brands);
        Assert.NotNull(context.Providers);
        Assert.NotNull(context.Persons);
        Assert.NotNull(context.Cities);
        Assert.NotNull(context.Provinces);
        Assert.NotNull(context.Orders);
        Assert.NotNull(context.OrderDetails);
        Assert.NotNull(context.PaymentMethods);
    }

    [Fact]
    public void EcommerceDbContext_Products_ShouldBeEmpty()
    {
        // Arrange
        var options = CreateNewContextOptions();

        // Act
        using var context = new EcommerceDbContext(options);

        // Assert
        Assert.Empty(context.Products);
    }

    [Fact]
    public void EcommerceDbContext_AddProduct_ShouldSaveSuccessfully()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        var product = new Product
        {
            Name = "Test Product",
            ProviderId = 1,
            BrandId = 1,
            CategoryId = 1,
            Stock = 10,
            UnitPrice = 99.99m,
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "TestUser"
        };

        // Act
        context.Products.Add(product);
        context.SaveChanges();

        // Assert
        Assert.Single(context.Products);
        Assert.Equal("Test Product", context.Products.First().Name);
    }

    [Fact]
    public void EcommerceDbContext_AddCategory_ShouldSaveSuccessfully()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        var category = new Category
        {
            Name = "Electronics",
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "TestUser"
        };

        // Act
        context.Categories.Add(category);
        context.SaveChanges();

        // Assert
        Assert.Single(context.Categories);
        Assert.Equal("Electronics", context.Categories.First().Name);
    }

    [Fact]
    public void EcommerceDbContext_AddBrand_ShouldSaveSuccessfully()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        var brand = new Brand
        {
            Name = "Apple",
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "TestUser"
        };

        // Act
        context.Brands.Add(brand);
        context.SaveChanges();

        // Assert
        Assert.Single(context.Brands);
        Assert.Equal("Apple", context.Brands.First().Name);
    }

    [Fact]
    public void EcommerceDbContext_AddProvider_ShouldSaveSuccessfully()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        var provider = new Provider
        {
            Name = "Tech Supplier",
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "TestUser"
        };

        // Act
        context.Providers.Add(provider);
        context.SaveChanges();

        // Assert
        Assert.Single(context.Providers);
        Assert.Equal("Tech Supplier", context.Providers.First().Name);
    }

    [Fact]
    public void EcommerceDbContext_AddPerson_ShouldSaveSuccessfully()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        var person = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Username = "johndoe",
            PasswordHash = "hash",
            Role = "Customer",
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "System"
        };

        // Act
        context.Persons.Add(person);
        context.SaveChanges();

        // Assert
        Assert.Single(context.Persons);
        Assert.Equal("John", context.Persons.First().FirstName);
    }

    [Fact]
    public void EcommerceDbContext_AddOrder_ShouldSaveSuccessfully()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        var order = new Order
        {
            PersonId = 1,
            OrderDate = DateTime.UtcNow,
            PaymentMethodId = 1,
            TotalAmount = 999.99m,
            Status = "Pending",
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "System"
        };

        // Act
        context.Orders.Add(order);
        context.SaveChanges();

        // Assert
        Assert.Single(context.Orders);
        Assert.Equal("Pending", context.Orders.First().Status);
    }
}
