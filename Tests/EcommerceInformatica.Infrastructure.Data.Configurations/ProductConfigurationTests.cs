using Xunit;
using Microsoft.EntityFrameworkCore;
using EcommerceInformatica.Infrastructure.Data;
using EcommerceInformatica.Infrastructure.Data.Configurations;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Infrastructure.Data.Configurations.Tests;

/// <summary>
/// Test class for ProductConfiguration
/// </summary>
public class ProductConfigurationTests
{
    private DbContextOptions<EcommerceDbContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void ProductConfiguration_Configure_ShouldSetTableName()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(Product));

        // Assert
        Assert.NotNull(entityType);
        Assert.Equal("Articulos", entityType.GetTableName());
    }

    [Fact]
    public void ProductConfiguration_Configure_ShouldSetPrimaryKey()
    {
        // Arrange
        var options = CreateNewContextOptions();
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
    public void ProductConfiguration_Configure_ShouldSetColumnNames()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(Product));
        var idProperty = entityType?.FindProperty("Id");
        var nameProperty = entityType?.FindProperty("Name");

        // Assert
        Assert.NotNull(idProperty);
        Assert.Equal("IdArticulo", idProperty.GetColumnName());
        Assert.NotNull(nameProperty);
        Assert.Equal("NombreArticulo", nameProperty.GetColumnName());
    }

    [Fact]
    public void ProductConfiguration_Configure_ShouldSetNameAsRequired()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(Product));
        var nameProperty = entityType?.FindProperty("Name");

        // Assert
        Assert.NotNull(nameProperty);
        Assert.False(nameProperty.IsNullable);
    }

    [Fact]
    public void ProductConfiguration_Configure_ShouldSetMaxLengthForName()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(Product));
        var nameProperty = entityType?.FindProperty("Name");

        // Assert
        Assert.NotNull(nameProperty);
        Assert.Equal(200, nameProperty.GetMaxLength());
    }

    [Fact]
    public void ProductConfiguration_Configure_ShouldSetPrecisionForUnitPrice()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(Product));
        var unitPriceProperty = entityType?.FindProperty("UnitPrice");

        // Assert
        Assert.NotNull(unitPriceProperty);
        Assert.Equal(18, unitPriceProperty.GetPrecision());
        Assert.Equal(2, unitPriceProperty.GetScale());
    }

    [Fact]
    public void ProductConfiguration_Configure_ShouldSetRelationships()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(Product));
        var providerNavigation = entityType?.FindNavigation("Provider");
        var brandNavigation = entityType?.FindNavigation("Brand");
        var categoryNavigation = entityType?.FindNavigation("Category");

        // Assert
        Assert.NotNull(providerNavigation);
        Assert.NotNull(brandNavigation);
        Assert.NotNull(categoryNavigation);
    }
}
