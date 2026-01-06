using Xunit;
using Microsoft.EntityFrameworkCore;
using EcommerceInformatica.Infrastructure.Data;
using EcommerceInformatica.Infrastructure.Data.Configurations;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Infrastructure.Data.Configurations.Tests;

/// <summary>
/// Test class for CategoryConfiguration
/// </summary>
public class CategoryConfigurationTests
{
    private DbContextOptions<EcommerceDbContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void CategoryConfiguration_Configure_ShouldSetTableName()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(Category));

        // Assert
        Assert.NotNull(entityType);
        Assert.Equal("Categorias", entityType.GetTableName());
    }

    [Fact]
    public void CategoryConfiguration_Configure_ShouldSetPrimaryKey()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(Category));
        var primaryKey = entityType?.FindPrimaryKey();

        // Assert
        Assert.NotNull(primaryKey);
        Assert.Single(primaryKey.Properties);
        Assert.Equal("Id", primaryKey.Properties.First().Name);
    }

    [Fact]
    public void CategoryConfiguration_Configure_ShouldSetColumnNames()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(Category));
        var idProperty = entityType?.FindProperty("Id");
        var nameProperty = entityType?.FindProperty("Name");

        // Assert
        Assert.NotNull(idProperty);
        Assert.Equal("IdCategoria", idProperty.GetColumnName());
        Assert.NotNull(nameProperty);
        Assert.Equal("NombreCategoria", nameProperty.GetColumnName());
    }

    [Fact]
    public void CategoryConfiguration_Configure_ShouldSetNameAsRequired()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(Category));
        var nameProperty = entityType?.FindProperty("Name");

        // Assert
        Assert.NotNull(nameProperty);
        Assert.False(nameProperty.IsNullable);
    }

    [Fact]
    public void CategoryConfiguration_Configure_ShouldSetMaxLengthForName()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(Category));
        var nameProperty = entityType?.FindProperty("Name");

        // Assert
        Assert.NotNull(nameProperty);
        Assert.Equal(100, nameProperty.GetMaxLength());
    }

    [Fact]
    public void CategoryConfiguration_Configure_ShouldSetDescriptionMaxLength()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using var context = new EcommerceDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(Category));
        var descriptionProperty = entityType?.FindProperty("Description");

        // Assert
        Assert.NotNull(descriptionProperty);
        Assert.Equal(500, descriptionProperty.GetMaxLength());
    }
}
