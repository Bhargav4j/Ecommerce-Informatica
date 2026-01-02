using Xunit;
using EcommerceApp.Domain.Entities;
using System;

namespace EcommerceApp.Domain.Entities.Tests;

public class BrandTests
{
    [Fact]
    public void Brand_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var brand = new Brand();

        // Assert
        Assert.NotNull(brand);
        Assert.Equal(0, brand.Id);
        Assert.Equal(string.Empty, brand.Name);
        Assert.Null(brand.Description);
        Assert.True(brand.IsActive);
        Assert.Equal(string.Empty, brand.CreatedBy);
        Assert.Null(brand.ModifiedBy);
        Assert.Null(brand.ModifiedDate);
        Assert.NotNull(brand.Products);
        Assert.Empty(brand.Products);
    }

    [Fact]
    public void Brand_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var brand = new Brand();
        var createdDate = DateTime.UtcNow;
        var modifiedDate = DateTime.UtcNow.AddDays(1);

        // Act
        brand.Id = 1;
        brand.Name = "Apple";
        brand.Description = "Apple Inc.";
        brand.IsActive = false;
        brand.CreatedDate = createdDate;
        brand.ModifiedDate = modifiedDate;
        brand.CreatedBy = "Admin";
        brand.ModifiedBy = "System";

        // Assert
        Assert.Equal(1, brand.Id);
        Assert.Equal("Apple", brand.Name);
        Assert.Equal("Apple Inc.", brand.Description);
        Assert.False(brand.IsActive);
        Assert.Equal(createdDate, brand.CreatedDate);
        Assert.Equal(modifiedDate, brand.ModifiedDate);
        Assert.Equal("Admin", brand.CreatedBy);
        Assert.Equal("System", brand.ModifiedBy);
    }

    [Fact]
    public void Brand_Products_ShouldBeInitializedAsEmptyList()
    {
        // Arrange & Act
        var brand = new Brand();

        // Assert
        Assert.NotNull(brand.Products);
        Assert.IsAssignableFrom<ICollection<Product>>(brand.Products);
        Assert.Empty(brand.Products);
    }

    [Fact]
    public void Brand_Products_ShouldAllowAddingProducts()
    {
        // Arrange
        var brand = new Brand { Id = 1 };
        var product = new Product { Id = 1, BrandId = brand.Id };

        // Act
        brand.Products.Add(product);

        // Assert
        Assert.Single(brand.Products);
        Assert.Contains(product, brand.Products);
    }

    [Fact]
    public void Brand_IsActive_ShouldDefaultToTrue()
    {
        // Arrange & Act
        var brand = new Brand();

        // Assert
        Assert.True(brand.IsActive);
    }

    [Fact]
    public void Brand_Description_ShouldBeNullableAndAcceptNull()
    {
        // Arrange
        var brand = new Brand();

        // Act
        brand.Description = null;

        // Assert
        Assert.Null(brand.Description);
    }

    [Fact]
    public void Brand_Name_ShouldAcceptStringValue()
    {
        // Arrange
        var brand = new Brand();
        var name = "Samsung";

        // Act
        brand.Name = name;

        // Assert
        Assert.Equal(name, brand.Name);
    }

    [Fact]
    public void Brand_ModifiedDate_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var brand = new Brand();

        // Assert
        Assert.Null(brand.ModifiedDate);
    }

    [Fact]
    public void Brand_ModifiedBy_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var brand = new Brand();

        // Assert
        Assert.Null(brand.ModifiedBy);
    }
}
