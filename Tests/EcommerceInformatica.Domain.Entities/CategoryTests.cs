using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Entities.Tests;

/// <summary>
/// Test class for Category entity
/// </summary>
public class CategoryTests
{
    [Fact]
    public void Category_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var category = new Category
        {
            Id = 1,
            Name = "Electronics",
            Description = "Electronic products",
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.Equal(1, category.Id);
        Assert.Equal("Electronics", category.Name);
        Assert.Equal("Electronic products", category.Description);
        Assert.True(category.IsActive);
        Assert.NotNull(category.CreatedBy);
    }

    [Fact]
    public void Category_Name_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var category = new Category
            {
                Name = null!,
                CreatedBy = "TestUser"
            };
        });
    }

    [Fact]
    public void Category_Products_ShouldInitializeAsEmptyList()
    {
        // Arrange & Act
        var category = new Category
        {
            Name = "Electronics",
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.NotNull(category.Products);
        Assert.Empty(category.Products);
    }

    [Fact]
    public void Category_Description_ShouldBeNullable()
    {
        // Arrange & Act
        var category = new Category
        {
            Name = "Electronics",
            CreatedBy = "TestUser",
            Description = null
        };

        // Assert
        Assert.Null(category.Description);
    }

    [Fact]
    public void Category_ModifiedDate_ShouldBeNullable()
    {
        // Arrange & Act
        var category = new Category
        {
            Name = "Electronics",
            CreatedBy = "TestUser",
            ModifiedDate = null
        };

        // Assert
        Assert.Null(category.ModifiedDate);
    }

    [Fact]
    public void Category_IsActive_ShouldDefaultToFalse()
    {
        // Arrange & Act
        var category = new Category
        {
            Name = "Electronics",
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.False(category.IsActive);
    }

    [Fact]
    public void Category_CreatedBy_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var category = new Category
            {
                Name = "Electronics",
                CreatedBy = null!
            };
        });
    }

    [Fact]
    public void Category_ModifiedBy_ShouldBeNullable()
    {
        // Arrange & Act
        var category = new Category
        {
            Name = "Electronics",
            CreatedBy = "TestUser",
            ModifiedBy = null
        };

        // Assert
        Assert.Null(category.ModifiedBy);
    }

    [Fact]
    public void Category_WithProducts_ShouldMaintainRelationship()
    {
        // Arrange
        var category = new Category
        {
            Name = "Electronics",
            CreatedBy = "TestUser"
        };

        var product = new Product
        {
            Name = "Laptop",
            CreatedBy = "TestUser",
            CategoryId = category.Id
        };

        // Act
        category.Products.Add(product);

        // Assert
        Assert.Single(category.Products);
        Assert.Equal(product, category.Products.First());
    }
}
