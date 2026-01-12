using Xunit;
using EcommerceInformatica.Domain.Entities;
using System;

namespace EcommerceInformatica.Domain.Tests;

public class InvoiceDetailTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var invoiceDetail = new InvoiceDetail();

        // Assert
        Assert.NotNull(invoiceDetail);
        Assert.Equal(0, invoiceDetail.Id);
        Assert.Equal(0, invoiceDetail.Quantity);
        Assert.Equal(0, invoiceDetail.UnitPrice);
        Assert.Equal(0, invoiceDetail.Subtotal);
        Assert.False(invoiceDetail.IsActive);
        Assert.Equal(string.Empty, invoiceDetail.CreatedBy);
        Assert.Null(invoiceDetail.ModifiedBy);
    }

    [Fact]
    public void SetId_ShouldSetValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.Id = 500;

        // Assert
        Assert.Equal(500, invoiceDetail.Id);
    }

    [Fact]
    public void SetQuantity_ShouldSetValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.Quantity = 10;

        // Assert
        Assert.Equal(10, invoiceDetail.Quantity);
    }

    [Fact]
    public void SetQuantity_WithZero_ShouldSetValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.Quantity = 0;

        // Assert
        Assert.Equal(0, invoiceDetail.Quantity);
    }

    [Fact]
    public void SetUnitPrice_ShouldSetValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.UnitPrice = 250.75m;

        // Assert
        Assert.Equal(250.75m, invoiceDetail.UnitPrice);
    }

    [Fact]
    public void SetUnitPrice_WithZero_ShouldSetValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.UnitPrice = 0m;

        // Assert
        Assert.Equal(0m, invoiceDetail.UnitPrice);
    }

    [Fact]
    public void SetSubtotal_ShouldSetValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.Subtotal = 2507.50m;

        // Assert
        Assert.Equal(2507.50m, invoiceDetail.Subtotal);
    }

    [Fact]
    public void SetIsActive_ShouldSetValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.IsActive = true;

        // Assert
        Assert.True(invoiceDetail.IsActive);
    }

    [Fact]
    public void SetCreatedDate_ShouldSetValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();
        var date = new DateTime(2026, 1, 12);

        // Act
        invoiceDetail.CreatedDate = date;

        // Assert
        Assert.Equal(date, invoiceDetail.CreatedDate);
    }

    [Fact]
    public void SetModifiedDate_ShouldSetValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();
        var date = new DateTime(2026, 1, 12);

        // Act
        invoiceDetail.ModifiedDate = date;

        // Assert
        Assert.Equal(date, invoiceDetail.ModifiedDate);
    }

    [Fact]
    public void SetModifiedDate_WithNull_ShouldSetNull()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.ModifiedDate = null;

        // Assert
        Assert.Null(invoiceDetail.ModifiedDate);
    }

    [Fact]
    public void SetCreatedBy_ShouldSetValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.CreatedBy = "admin";

        // Assert
        Assert.Equal("admin", invoiceDetail.CreatedBy);
    }

    [Fact]
    public void SetModifiedBy_ShouldSetValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.ModifiedBy = "user";

        // Assert
        Assert.Equal("user", invoiceDetail.ModifiedBy);
    }

    [Fact]
    public void SetModifiedBy_WithNull_ShouldSetNull()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.ModifiedBy = null;

        // Assert
        Assert.Null(invoiceDetail.ModifiedBy);
    }

    [Fact]
    public void SetInvoiceId_ShouldSetValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.InvoiceId = 100;

        // Assert
        Assert.Equal(100, invoiceDetail.InvoiceId);
    }

    [Fact]
    public void SetInvoice_ShouldSetValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();
        var invoice = new Invoice { Id = 50, TotalAmount = 1000m };

        // Act
        invoiceDetail.Invoice = invoice;

        // Assert
        Assert.NotNull(invoiceDetail.Invoice);
        Assert.Equal(50, invoiceDetail.Invoice.Id);
        Assert.Equal(1000m, invoiceDetail.Invoice.TotalAmount);
    }

    [Fact]
    public void SetProductId_ShouldSetValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.ProductId = 200;

        // Assert
        Assert.Equal(200, invoiceDetail.ProductId);
    }

    [Fact]
    public void SetProduct_ShouldSetValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();
        var product = new Product { Id = 75, Name = "Laptop" };

        // Act
        invoiceDetail.Product = product;

        // Assert
        Assert.NotNull(invoiceDetail.Product);
        Assert.Equal(75, invoiceDetail.Product.Id);
        Assert.Equal("Laptop", invoiceDetail.Product.Name);
    }
}
