using Xunit;
using EcommerceInformatica.Domain.Entities;
using System;
using System.Collections.Generic;

namespace EcommerceInformatica.Domain.Tests;

public class SupplierTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var supplier = new Supplier();

        // Assert
        Assert.NotNull(supplier);
        Assert.Equal(0, supplier.Id);
        Assert.Equal(string.Empty, supplier.Name);
        Assert.Equal(string.Empty, supplier.Description);
        Assert.Equal(string.Empty, supplier.ContactName);
        Assert.Equal(string.Empty, supplier.ContactPhone);
        Assert.Equal(string.Empty, supplier.ContactEmail);
        Assert.False(supplier.IsActive);
        Assert.Equal(string.Empty, supplier.CreatedBy);
        Assert.Null(supplier.ModifiedBy);
        Assert.NotNull(supplier.Products);
    }

    [Fact]
    public void SetId_ShouldSetValue()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.Id = 75;

        // Assert
        Assert.Equal(75, supplier.Id);
    }

    [Fact]
    public void SetName_ShouldSetValue()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.Name = "Global Supplies Inc";

        // Assert
        Assert.Equal("Global Supplies Inc", supplier.Name);
    }

    [Fact]
    public void SetDescription_ShouldSetValue()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.Description = "Leading supplier of electronics";

        // Assert
        Assert.Equal("Leading supplier of electronics", supplier.Description);
    }

    [Fact]
    public void SetContactName_ShouldSetValue()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.ContactName = "John Doe";

        // Assert
        Assert.Equal("John Doe", supplier.ContactName);
    }

    [Fact]
    public void SetContactPhone_ShouldSetValue()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.ContactPhone = "+1234567890";

        // Assert
        Assert.Equal("+1234567890", supplier.ContactPhone);
    }

    [Fact]
    public void SetContactEmail_ShouldSetValue()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.ContactEmail = "contact@supplier.com";

        // Assert
        Assert.Equal("contact@supplier.com", supplier.ContactEmail);
    }

    [Fact]
    public void SetIsActive_ShouldSetValue()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.IsActive = true;

        // Assert
        Assert.True(supplier.IsActive);
    }

    [Fact]
    public void SetCreatedDate_ShouldSetValue()
    {
        // Arrange
        var supplier = new Supplier();
        var date = new DateTime(2026, 1, 12);

        // Act
        supplier.CreatedDate = date;

        // Assert
        Assert.Equal(date, supplier.CreatedDate);
    }

    [Fact]
    public void SetModifiedDate_ShouldSetValue()
    {
        // Arrange
        var supplier = new Supplier();
        var date = new DateTime(2026, 1, 12);

        // Act
        supplier.ModifiedDate = date;

        // Assert
        Assert.Equal(date, supplier.ModifiedDate);
    }

    [Fact]
    public void SetModifiedDate_WithNull_ShouldSetNull()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.ModifiedDate = null;

        // Assert
        Assert.Null(supplier.ModifiedDate);
    }

    [Fact]
    public void SetCreatedBy_ShouldSetValue()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.CreatedBy = "admin";

        // Assert
        Assert.Equal("admin", supplier.CreatedBy);
    }

    [Fact]
    public void SetModifiedBy_ShouldSetValue()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.ModifiedBy = "user";

        // Assert
        Assert.Equal("user", supplier.ModifiedBy);
    }

    [Fact]
    public void SetModifiedBy_WithNull_ShouldSetNull()
    {
        // Arrange
        var supplier = new Supplier();

        // Act
        supplier.ModifiedBy = null;

        // Assert
        Assert.Null(supplier.ModifiedBy);
    }

    [Fact]
    public void SetProducts_ShouldSetCollection()
    {
        // Arrange
        var supplier = new Supplier();
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product A" },
            new Product { Id = 2, Name = "Product B" }
        };

        // Act
        supplier.Products = products;

        // Assert
        Assert.NotNull(supplier.Products);
        Assert.Equal(2, supplier.Products.Count);
    }

    [Fact]
    public void Products_ShouldBeEmptyListByDefault()
    {
        // Arrange & Act
        var supplier = new Supplier();

        // Assert
        Assert.NotNull(supplier.Products);
        Assert.Empty(supplier.Products);
    }
}
