using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Entities.Tests;

/// <summary>
/// Test class for Brand entity
/// </summary>
public class BrandTests
{
    [Fact]
    public void Brand_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var brand = new Brand
        {
            Id = 1,
            Name = "Apple",
            Description = "Apple Inc.",
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.Equal(1, brand.Id);
        Assert.Equal("Apple", brand.Name);
        Assert.Equal("Apple Inc.", brand.Description);
        Assert.True(brand.IsActive);
        Assert.NotNull(brand.CreatedBy);
    }

    [Fact]
    public void Brand_Name_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var brand = new Brand
            {
                Name = null!,
                CreatedBy = "TestUser"
            };
        });
    }

    [Fact]
    public void Brand_Products_ShouldInitializeAsEmptyList()
    {
        // Arrange & Act
        var brand = new Brand
        {
            Name = "Apple",
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.NotNull(brand.Products);
        Assert.Empty(brand.Products);
    }

    [Fact]
    public void Brand_Description_ShouldBeNullable()
    {
        // Arrange & Act
        var brand = new Brand
        {
            Name = "Apple",
            CreatedBy = "TestUser",
            Description = null
        };

        // Assert
        Assert.Null(brand.Description);
    }

    [Fact]
    public void Brand_ModifiedDate_ShouldBeNullable()
    {
        // Arrange & Act
        var brand = new Brand
        {
            Name = "Apple",
            CreatedBy = "TestUser",
            ModifiedDate = null
        };

        // Assert
        Assert.Null(brand.ModifiedDate);
    }

    [Fact]
    public void Brand_IsActive_ShouldDefaultToFalse()
    {
        // Arrange & Act
        var brand = new Brand
        {
            Name = "Apple",
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.False(brand.IsActive);
    }

    [Fact]
    public void Brand_CreatedBy_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var brand = new Brand
            {
                Name = "Apple",
                CreatedBy = null!
            };
        });
    }

    [Fact]
    public void Brand_ModifiedBy_ShouldBeNullable()
    {
        // Arrange & Act
        var brand = new Brand
        {
            Name = "Apple",
            CreatedBy = "TestUser",
            ModifiedBy = null
        };

        // Assert
        Assert.Null(brand.ModifiedBy);
    }

    [Fact]
    public void Brand_WithProducts_ShouldMaintainRelationship()
    {
        // Arrange
        var brand = new Brand
        {
            Name = "Apple",
            CreatedBy = "TestUser"
        };

        var product = new Product
        {
            Name = "MacBook Pro",
            CreatedBy = "TestUser",
            BrandId = brand.Id
        };

        // Act
        brand.Products.Add(product);

        // Assert
        Assert.Single(brand.Products);
        Assert.Equal(product, brand.Products.First());
    }
}
