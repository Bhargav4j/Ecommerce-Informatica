using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Entities.Tests;

/// <summary>
/// Test class for Product entity
/// </summary>
public class ProductTests
{
    [Fact]
    public void Product_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var product = new Product
        {
            Id = 1,
            Name = "Test Product",
            Description = "Test Description",
            ProviderId = 1,
            BrandId = 1,
            CategoryId = 1,
            Stock = 10,
            UnitPrice = 99.99m,
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.Equal(1, product.Id);
        Assert.Equal("Test Product", product.Name);
        Assert.Equal("Test Description", product.Description);
        Assert.Equal(1, product.ProviderId);
        Assert.Equal(1, product.BrandId);
        Assert.Equal(1, product.CategoryId);
        Assert.Equal(10, product.Stock);
        Assert.Equal(99.99m, product.UnitPrice);
        Assert.True(product.IsActive);
        Assert.NotNull(product.CreatedBy);
    }

    [Fact]
    public void Product_Name_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var product = new Product
            {
                Name = null!,
                CreatedBy = "TestUser"
            };
        });
    }

    [Fact]
    public void Product_OrderDetails_ShouldInitializeAsEmptyList()
    {
        // Arrange & Act
        var product = new Product
        {
            Name = "Test Product",
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.NotNull(product.OrderDetails);
        Assert.Empty(product.OrderDetails);
    }

    [Fact]
    public void Product_ModifiedDate_ShouldBeNullable()
    {
        // Arrange & Act
        var product = new Product
        {
            Name = "Test Product",
            CreatedBy = "TestUser",
            ModifiedDate = null
        };

        // Assert
        Assert.Null(product.ModifiedDate);
    }

    [Fact]
    public void Product_UnitPrice_ShouldAcceptDecimalValues()
    {
        // Arrange & Act
        var product = new Product
        {
            Name = "Test Product",
            CreatedBy = "TestUser",
            UnitPrice = 12345.67m
        };

        // Assert
        Assert.Equal(12345.67m, product.UnitPrice);
    }

    [Fact]
    public void Product_Stock_ShouldAcceptZeroValue()
    {
        // Arrange & Act
        var product = new Product
        {
            Name = "Test Product",
            CreatedBy = "TestUser",
            Stock = 0
        };

        // Assert
        Assert.Equal(0, product.Stock);
    }

    [Fact]
    public void Product_Stock_ShouldAcceptNegativeValue()
    {
        // Arrange & Act
        var product = new Product
        {
            Name = "Test Product",
            CreatedBy = "TestUser",
            Stock = -5
        };

        // Assert
        Assert.Equal(-5, product.Stock);
    }

    [Fact]
    public void Product_NavigationProperties_ShouldBeNullable()
    {
        // Arrange & Act
        var product = new Product
        {
            Name = "Test Product",
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.Null(product.Provider);
        Assert.Null(product.Brand);
        Assert.Null(product.Category);
    }

    [Fact]
    public void Product_IsActive_ShouldDefaultToFalse()
    {
        // Arrange & Act
        var product = new Product
        {
            Name = "Test Product",
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.False(product.IsActive);
    }

    [Fact]
    public void Product_Description_ShouldBeNullable()
    {
        // Arrange & Act
        var product = new Product
        {
            Name = "Test Product",
            CreatedBy = "TestUser",
            Description = null
        };

        // Assert
        Assert.Null(product.Description);
    }
}
