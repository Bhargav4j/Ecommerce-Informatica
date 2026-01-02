using Xunit;
using EcommerceApp.Domain.Entities;
using System;

namespace EcommerceApp.Domain.Entities.Tests;

public class CategoryTests
{
    [Fact]
    public void Category_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var category = new Category();

        // Assert
        Assert.NotNull(category);
        Assert.Equal(0, category.Id);
        Assert.Equal(string.Empty, category.Name);
        Assert.Null(category.Description);
        Assert.True(category.IsActive);
        Assert.Equal(string.Empty, category.CreatedBy);
        Assert.Null(category.ModifiedBy);
        Assert.Null(category.ModifiedDate);
        Assert.NotNull(category.Products);
        Assert.Empty(category.Products);
    }

    [Fact]
    public void Category_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var category = new Category();
        var createdDate = DateTime.UtcNow;
        var modifiedDate = DateTime.UtcNow.AddDays(1);

        // Act
        category.Id = 1;
        category.Name = "Electronics";
        category.Description = "Electronic items";
        category.IsActive = false;
        category.CreatedDate = createdDate;
        category.ModifiedDate = modifiedDate;
        category.CreatedBy = "Admin";
        category.ModifiedBy = "System";

        // Assert
        Assert.Equal(1, category.Id);
        Assert.Equal("Electronics", category.Name);
        Assert.Equal("Electronic items", category.Description);
        Assert.False(category.IsActive);
        Assert.Equal(createdDate, category.CreatedDate);
        Assert.Equal(modifiedDate, category.ModifiedDate);
        Assert.Equal("Admin", category.CreatedBy);
        Assert.Equal("System", category.ModifiedBy);
    }

    [Fact]
    public void Category_Products_ShouldBeInitializedAsEmptyList()
    {
        // Arrange & Act
        var category = new Category();

        // Assert
        Assert.NotNull(category.Products);
        Assert.IsAssignableFrom<ICollection<Product>>(category.Products);
        Assert.Empty(category.Products);
    }

    [Fact]
    public void Category_Products_ShouldAllowAddingProducts()
    {
        // Arrange
        var category = new Category { Id = 1 };
        var product = new Product { Id = 1, CategoryId = category.Id };

        // Act
        category.Products.Add(product);

        // Assert
        Assert.Single(category.Products);
        Assert.Contains(product, category.Products);
    }

    [Fact]
    public void Category_IsActive_ShouldDefaultToTrue()
    {
        // Arrange & Act
        var category = new Category();

        // Assert
        Assert.True(category.IsActive);
    }

    [Fact]
    public void Category_Description_ShouldBeNullableAndAcceptNull()
    {
        // Arrange
        var category = new Category();

        // Act
        category.Description = null;

        // Assert
        Assert.Null(category.Description);
    }

    [Fact]
    public void Category_Description_ShouldAcceptStringValue()
    {
        // Arrange
        var category = new Category();
        var description = "Test Description";

        // Act
        category.Description = description;

        // Assert
        Assert.Equal(description, category.Description);
    }

    [Fact]
    public void Category_Name_ShouldAcceptEmptyString()
    {
        // Arrange
        var category = new Category();

        // Act
        category.Name = string.Empty;

        // Assert
        Assert.Equal(string.Empty, category.Name);
    }

    [Fact]
    public void Category_ModifiedDate_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var category = new Category();

        // Assert
        Assert.Null(category.ModifiedDate);
    }

    [Fact]
    public void Category_ModifiedBy_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var category = new Category();

        // Assert
        Assert.Null(category.ModifiedBy);
    }
}
