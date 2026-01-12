using Xunit;
using EcommerceInformatica.Domain.Entities;
using System;
using System.Collections.Generic;

namespace EcommerceInformatica.Domain.Tests;

public class CategoryTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var category = new Category();

        // Assert
        Assert.NotNull(category);
        Assert.Equal(0, category.Id);
        Assert.Equal(string.Empty, category.Name);
        Assert.Equal(string.Empty, category.Description);
        Assert.False(category.IsActive);
        Assert.Equal(string.Empty, category.CreatedBy);
        Assert.Null(category.ModifiedBy);
        Assert.NotNull(category.Products);
    }

    [Fact]
    public void SetId_ShouldSetValue()
    {
        // Arrange
        var category = new Category();

        // Act
        category.Id = 100;

        // Assert
        Assert.Equal(100, category.Id);
    }

    [Fact]
    public void SetName_ShouldSetValue()
    {
        // Arrange
        var category = new Category();

        // Act
        category.Name = "Electronics";

        // Assert
        Assert.Equal("Electronics", category.Name);
    }

    [Fact]
    public void SetDescription_ShouldSetValue()
    {
        // Arrange
        var category = new Category();

        // Act
        category.Description = "Electronic devices and accessories";

        // Assert
        Assert.Equal("Electronic devices and accessories", category.Description);
    }

    [Fact]
    public void SetIsActive_ShouldSetValue()
    {
        // Arrange
        var category = new Category();

        // Act
        category.IsActive = true;

        // Assert
        Assert.True(category.IsActive);
    }

    [Fact]
    public void SetCreatedDate_ShouldSetValue()
    {
        // Arrange
        var category = new Category();
        var date = new DateTime(2026, 1, 12);

        // Act
        category.CreatedDate = date;

        // Assert
        Assert.Equal(date, category.CreatedDate);
    }

    [Fact]
    public void SetModifiedDate_ShouldSetValue()
    {
        // Arrange
        var category = new Category();
        var date = new DateTime(2026, 1, 12);

        // Act
        category.ModifiedDate = date;

        // Assert
        Assert.Equal(date, category.ModifiedDate);
    }

    [Fact]
    public void SetModifiedDate_WithNull_ShouldSetNull()
    {
        // Arrange
        var category = new Category();

        // Act
        category.ModifiedDate = null;

        // Assert
        Assert.Null(category.ModifiedDate);
    }

    [Fact]
    public void SetCreatedBy_ShouldSetValue()
    {
        // Arrange
        var category = new Category();

        // Act
        category.CreatedBy = "admin";

        // Assert
        Assert.Equal("admin", category.CreatedBy);
    }

    [Fact]
    public void SetModifiedBy_ShouldSetValue()
    {
        // Arrange
        var category = new Category();

        // Act
        category.ModifiedBy = "user";

        // Assert
        Assert.Equal("user", category.ModifiedBy);
    }

    [Fact]
    public void SetModifiedBy_WithNull_ShouldSetNull()
    {
        // Arrange
        var category = new Category();

        // Act
        category.ModifiedBy = null;

        // Assert
        Assert.Null(category.ModifiedBy);
    }

    [Fact]
    public void SetProducts_ShouldSetCollection()
    {
        // Arrange
        var category = new Category();
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1" },
            new Product { Id = 2, Name = "Product 2" }
        };

        // Act
        category.Products = products;

        // Assert
        Assert.NotNull(category.Products);
        Assert.Equal(2, category.Products.Count);
    }

    [Fact]
    public void Products_ShouldBeEmptyListByDefault()
    {
        // Arrange & Act
        var category = new Category();

        // Assert
        Assert.NotNull(category.Products);
        Assert.Empty(category.Products);
    }
}
