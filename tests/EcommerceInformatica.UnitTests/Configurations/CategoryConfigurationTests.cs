using Xunit;
using Microsoft.EntityFrameworkCore;
using EcommerceInformatica.Infrastructure.Data;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.UnitTests.Configurations;

public class CategoryConfigurationTests
{
    private DbContextOptions<EcommerceDbContext> CreateInMemoryOptions()
    {
        return new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public void CategoryConfiguration_ShouldConfigureEntity()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(Category));

        // Assert
        Assert.NotNull(entityType);
    }

    [Fact]
    public void CategoryConfiguration_ShouldSetPrimaryKey()
    {
        // Arrange
        var options = CreateInMemoryOptions();
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
    public void CategoryConfiguration_ShouldMapProperties()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);

        // Act
        var entityType = context.Model.FindEntityType(typeof(Category));
        var nameProperty = entityType?.FindProperty("Name");

        // Assert
        Assert.NotNull(nameProperty);
    }

    [Fact]
    public void CategoryConfiguration_ShouldAllowCategoryToBeAdded()
    {
        // Arrange
        var options = CreateInMemoryOptions();
        using var context = new EcommerceDbContext(options);
        var category = new Category { Id = 1, Name = "Test Category", Description = "Test", CreatedBy = "Admin" };

        // Act
        context.Categories.Add(category);
        context.SaveChanges();
        var result = context.Categories.Find(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Category", result.Name);
    }
}
