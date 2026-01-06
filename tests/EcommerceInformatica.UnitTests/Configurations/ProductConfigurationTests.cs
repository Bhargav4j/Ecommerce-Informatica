using Xunit;
using Microsoft.EntityFrameworkCore;
using EcommerceInformatica.Infrastructure.Data;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.UnitTests.Configurations;

public class ProductConfigurationTests
{
    private DbContextOptions<EcommerceDbContext> CreateInMemoryOptions()
    {
        return new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void ProductConfiguration_ShouldConfigureEntity()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(Product));

        // Assert
        Assert.NotNull(entityType);
    }

    [Fact]
    public void ProductConfiguration_ShouldSetPrimaryKey()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(Product));
        var primaryKey = entityType?.FindPrimaryKey();

        // Assert
        Assert.NotNull(primaryKey);
        Assert.Single(primaryKey.Properties);
        Assert.Equal("Id", primaryKey.Properties.First().Name);
    }

    [Fact]
    public void ProductConfiguration_ShouldMapProperties()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(Product));
        var nameProperty = entityType?.FindProperty("Name");
        var unitPriceProperty = entityType?.FindProperty("UnitPrice");

        // Assert
        Assert.NotNull(nameProperty);
        Assert.NotNull(unitPriceProperty);
    }

    [Fact]
    public void ProductConfiguration_ShouldAllowProductToBeAdded()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var product = new Product
        {
            Id = 1,
            Name = "Test Product",
            UnitPrice = 100,
            Stock = 10,
            CategoryId = 1,
            BrandId = 1,
            SupplierId = 1,
            CreatedBy = "Admin"
        };

        // Act
        context.Products.Add(product);
        context.SaveChanges();
        var result = context.Products.Find(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Product", result.Name);
        Assert.Equal(100, result.UnitPrice);
    }
}
