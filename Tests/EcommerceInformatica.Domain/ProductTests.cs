using Xunit;
using EcommerceInformatica.Domain.Entities;
using System;
using System.Collections.Generic;

namespace EcommerceInformatica.Domain.Tests;

public class ProductTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var product = new Product();

        // Assert
        Assert.NotNull(product);
        Assert.Equal(0, product.Id);
        Assert.Equal(string.Empty, product.Name);
        Assert.Equal(string.Empty, product.Description);
        Assert.Equal(0, product.UnitPrice);
        Assert.Equal(0, product.Stock);
        Assert.False(product.IsActive);
        Assert.Equal(string.Empty, product.CreatedBy);
        Assert.Null(product.ModifiedBy);
        Assert.NotNull(product.InvoiceDetails);
    }

    [Fact]
    public void SetId_ShouldSetValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Id = 123;

        // Assert
        Assert.Equal(123, product.Id);
    }

    [Fact]
    public void SetName_ShouldSetValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Name = "Test Product";

        // Assert
        Assert.Equal("Test Product", product.Name);
    }

    [Fact]
    public void SetDescription_ShouldSetValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Description = "Test Description";

        // Assert
        Assert.Equal("Test Description", product.Description);
    }

    [Fact]
    public void SetUnitPrice_ShouldSetValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.UnitPrice = 99.99m;

        // Assert
        Assert.Equal(99.99m, product.UnitPrice);
    }

    [Fact]
    public void SetUnitPrice_WithZero_ShouldSetValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.UnitPrice = 0m;

        // Assert
        Assert.Equal(0m, product.UnitPrice);
    }

    [Fact]
    public void SetStock_ShouldSetValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Stock = 50;

        // Assert
        Assert.Equal(50, product.Stock);
    }

    [Fact]
    public void SetStock_WithZero_ShouldSetValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.Stock = 0;

        // Assert
        Assert.Equal(0, product.Stock);
    }

    [Fact]
    public void SetIsActive_ShouldSetValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.IsActive = true;

        // Assert
        Assert.True(product.IsActive);
    }

    [Fact]
    public void SetCreatedDate_ShouldSetValue()
    {
        // Arrange
        var product = new Product();
        var date = new DateTime(2026, 1, 12);

        // Act
        product.CreatedDate = date;

        // Assert
        Assert.Equal(date, product.CreatedDate);
    }

    [Fact]
    public void SetModifiedDate_ShouldSetValue()
    {
        // Arrange
        var product = new Product();
        var date = new DateTime(2026, 1, 12);

        // Act
        product.ModifiedDate = date;

        // Assert
        Assert.Equal(date, product.ModifiedDate);
    }

    [Fact]
    public void SetModifiedDate_WithNull_ShouldSetNull()
    {
        // Arrange
        var product = new Product();

        // Act
        product.ModifiedDate = null;

        // Assert
        Assert.Null(product.ModifiedDate);
    }

    [Fact]
    public void SetCreatedBy_ShouldSetValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.CreatedBy = "admin";

        // Assert
        Assert.Equal("admin", product.CreatedBy);
    }

    [Fact]
    public void SetModifiedBy_ShouldSetValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.ModifiedBy = "user";

        // Assert
        Assert.Equal("user", product.ModifiedBy);
    }

    [Fact]
    public void SetModifiedBy_WithNull_ShouldSetNull()
    {
        // Arrange
        var product = new Product();

        // Act
        product.ModifiedBy = null;

        // Assert
        Assert.Null(product.ModifiedBy);
    }

    [Fact]
    public void SetCategoryId_ShouldSetValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.CategoryId = 10;

        // Assert
        Assert.Equal(10, product.CategoryId);
    }

    [Fact]
    public void SetCategory_ShouldSetValue()
    {
        // Arrange
        var product = new Product();
        var category = new Category { Id = 1, Name = "Electronics" };

        // Act
        product.Category = category;

        // Assert
        Assert.NotNull(product.Category);
        Assert.Equal(1, product.Category.Id);
        Assert.Equal("Electronics", product.Category.Name);
    }

    [Fact]
    public void SetBrandId_ShouldSetValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.BrandId = 20;

        // Assert
        Assert.Equal(20, product.BrandId);
    }

    [Fact]
    public void SetBrand_ShouldSetValue()
    {
        // Arrange
        var product = new Product();
        var brand = new Brand { Id = 2, Name = "BrandName" };

        // Act
        product.Brand = brand;

        // Assert
        Assert.NotNull(product.Brand);
        Assert.Equal(2, product.Brand.Id);
        Assert.Equal("BrandName", product.Brand.Name);
    }

    [Fact]
    public void SetSupplierId_ShouldSetValue()
    {
        // Arrange
        var product = new Product();

        // Act
        product.SupplierId = 30;

        // Assert
        Assert.Equal(30, product.SupplierId);
    }

    [Fact]
    public void SetSupplier_ShouldSetValue()
    {
        // Arrange
        var product = new Product();
        var supplier = new Supplier { Id = 3, Name = "Supplier Corp" };

        // Act
        product.Supplier = supplier;

        // Assert
        Assert.NotNull(product.Supplier);
        Assert.Equal(3, product.Supplier.Id);
        Assert.Equal("Supplier Corp", product.Supplier.Name);
    }

    [Fact]
    public void SetInvoiceDetails_ShouldSetCollection()
    {
        // Arrange
        var product = new Product();
        var invoiceDetails = new List<InvoiceDetail>
        {
            new InvoiceDetail { Id = 1, Quantity = 5 },
            new InvoiceDetail { Id = 2, Quantity = 3 }
        };

        // Act
        product.InvoiceDetails = invoiceDetails;

        // Assert
        Assert.NotNull(product.InvoiceDetails);
        Assert.Equal(2, product.InvoiceDetails.Count);
    }

    [Fact]
    public void InvoiceDetails_ShouldBeEmptyListByDefault()
    {
        // Arrange & Act
        var product = new Product();

        // Assert
        Assert.NotNull(product.InvoiceDetails);
        Assert.Empty(product.InvoiceDetails);
    }
}
