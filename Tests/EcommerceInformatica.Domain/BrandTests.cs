using Xunit;
using EcommerceInformatica.Domain.Entities;
using System;
using System.Collections.Generic;

namespace EcommerceInformatica.Domain.Tests;

public class BrandTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var brand = new Brand();

        // Assert
        Assert.NotNull(brand);
        Assert.Equal(0, brand.Id);
        Assert.Equal(string.Empty, brand.Name);
        Assert.Equal(string.Empty, brand.Description);
        Assert.False(brand.IsActive);
        Assert.Equal(string.Empty, brand.CreatedBy);
        Assert.Null(brand.ModifiedBy);
        Assert.NotNull(brand.Products);
    }

    [Fact]
    public void SetId_ShouldSetValue()
    {
        // Arrange
        var brand = new Brand();

        // Act
        brand.Id = 50;

        // Assert
        Assert.Equal(50, brand.Id);
    }

    [Fact]
    public void SetName_ShouldSetValue()
    {
        // Arrange
        var brand = new Brand();

        // Act
        brand.Name = "Apple";

        // Assert
        Assert.Equal("Apple", brand.Name);
    }

    [Fact]
    public void SetDescription_ShouldSetValue()
    {
        // Arrange
        var brand = new Brand();

        // Act
        brand.Description = "Premium electronics manufacturer";

        // Assert
        Assert.Equal("Premium electronics manufacturer", brand.Description);
    }

    [Fact]
    public void SetIsActive_ShouldSetValue()
    {
        // Arrange
        var brand = new Brand();

        // Act
        brand.IsActive = true;

        // Assert
        Assert.True(brand.IsActive);
    }

    [Fact]
    public void SetCreatedDate_ShouldSetValue()
    {
        // Arrange
        var brand = new Brand();
        var date = new DateTime(2026, 1, 12);

        // Act
        brand.CreatedDate = date;

        // Assert
        Assert.Equal(date, brand.CreatedDate);
    }

    [Fact]
    public void SetModifiedDate_ShouldSetValue()
    {
        // Arrange
        var brand = new Brand();
        var date = new DateTime(2026, 1, 12);

        // Act
        brand.ModifiedDate = date;

        // Assert
        Assert.Equal(date, brand.ModifiedDate);
    }

    [Fact]
    public void SetModifiedDate_WithNull_ShouldSetNull()
    {
        // Arrange
        var brand = new Brand();

        // Act
        brand.ModifiedDate = null;

        // Assert
        Assert.Null(brand.ModifiedDate);
    }

    [Fact]
    public void SetCreatedBy_ShouldSetValue()
    {
        // Arrange
        var brand = new Brand();

        // Act
        brand.CreatedBy = "admin";

        // Assert
        Assert.Equal("admin", brand.CreatedBy);
    }

    [Fact]
    public void SetModifiedBy_ShouldSetValue()
    {
        // Arrange
        var brand = new Brand();

        // Act
        brand.ModifiedBy = "user";

        // Assert
        Assert.Equal("user", brand.ModifiedBy);
    }

    [Fact]
    public void SetModifiedBy_WithNull_ShouldSetNull()
    {
        // Arrange
        var brand = new Brand();

        // Act
        brand.ModifiedBy = null;

        // Assert
        Assert.Null(brand.ModifiedBy);
    }

    [Fact]
    public void SetProducts_ShouldSetCollection()
    {
        // Arrange
        var brand = new Brand();
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "iPhone" },
            new Product { Id = 2, Name = "MacBook" }
        };

        // Act
        brand.Products = products;

        // Assert
        Assert.NotNull(brand.Products);
        Assert.Equal(2, brand.Products.Count);
    }

    [Fact]
    public void Products_ShouldBeEmptyListByDefault()
    {
        // Arrange & Act
        var brand = new Brand();

        // Assert
        Assert.NotNull(brand.Products);
        Assert.Empty(brand.Products);
    }
}
