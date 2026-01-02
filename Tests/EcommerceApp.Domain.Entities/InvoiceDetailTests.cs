using Xunit;
using EcommerceApp.Domain.Entities;
using System;

namespace EcommerceApp.Domain.Entities.Tests;

public class InvoiceDetailTests
{
    [Fact]
    public void InvoiceDetail_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var invoiceDetail = new InvoiceDetail();

        // Assert
        Assert.NotNull(invoiceDetail);
        Assert.Equal(0, invoiceDetail.InvoiceId);
        Assert.Equal(0, invoiceDetail.ProductId);
        Assert.Equal(0, invoiceDetail.Quantity);
        Assert.Equal(0, invoiceDetail.UnitPrice);
        Assert.Equal(0, invoiceDetail.Subtotal);
        Assert.Equal(string.Empty, invoiceDetail.CreatedBy);
        Assert.Null(invoiceDetail.ModifiedBy);
        Assert.Null(invoiceDetail.ModifiedDate);
    }

    [Fact]
    public void InvoiceDetail_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();
        var createdDate = DateTime.UtcNow;
        var modifiedDate = DateTime.UtcNow.AddDays(1);
        var invoice = new Invoice { Id = 1 };
        var product = new Product { Id = 1, Name = "Test Product" };

        // Act
        invoiceDetail.InvoiceId = 1;
        invoiceDetail.Invoice = invoice;
        invoiceDetail.ProductId = 1;
        invoiceDetail.Product = product;
        invoiceDetail.Quantity = 5;
        invoiceDetail.UnitPrice = 99.99m;
        invoiceDetail.Subtotal = 499.95m;
        invoiceDetail.CreatedDate = createdDate;
        invoiceDetail.ModifiedDate = modifiedDate;
        invoiceDetail.CreatedBy = "Admin";
        invoiceDetail.ModifiedBy = "System";

        // Assert
        Assert.Equal(1, invoiceDetail.InvoiceId);
        Assert.Equal(invoice, invoiceDetail.Invoice);
        Assert.Equal(1, invoiceDetail.ProductId);
        Assert.Equal(product, invoiceDetail.Product);
        Assert.Equal(5, invoiceDetail.Quantity);
        Assert.Equal(99.99m, invoiceDetail.UnitPrice);
        Assert.Equal(499.95m, invoiceDetail.Subtotal);
        Assert.Equal(createdDate, invoiceDetail.CreatedDate);
        Assert.Equal(modifiedDate, invoiceDetail.ModifiedDate);
        Assert.Equal("Admin", invoiceDetail.CreatedBy);
        Assert.Equal("System", invoiceDetail.ModifiedBy);
    }

    [Fact]
    public void InvoiceDetail_Quantity_ShouldAcceptPositiveInteger()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.Quantity = 10;

        // Assert
        Assert.Equal(10, invoiceDetail.Quantity);
    }

    [Fact]
    public void InvoiceDetail_Quantity_ShouldAcceptZeroValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.Quantity = 0;

        // Assert
        Assert.Equal(0, invoiceDetail.Quantity);
    }

    [Fact]
    public void InvoiceDetail_UnitPrice_ShouldAcceptDecimalValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.UnitPrice = 25.50m;

        // Assert
        Assert.Equal(25.50m, invoiceDetail.UnitPrice);
    }

    [Fact]
    public void InvoiceDetail_UnitPrice_ShouldAcceptZeroValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.UnitPrice = 0m;

        // Assert
        Assert.Equal(0m, invoiceDetail.UnitPrice);
    }

    [Fact]
    public void InvoiceDetail_Subtotal_ShouldAcceptDecimalValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.Subtotal = 127.50m;

        // Assert
        Assert.Equal(127.50m, invoiceDetail.Subtotal);
    }

    [Fact]
    public void InvoiceDetail_Subtotal_ShouldBeCalculatedCorrectly()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.Quantity = 3;
        invoiceDetail.UnitPrice = 50.00m;
        invoiceDetail.Subtotal = invoiceDetail.Quantity * invoiceDetail.UnitPrice;

        // Assert
        Assert.Equal(150.00m, invoiceDetail.Subtotal);
    }

    [Fact]
    public void InvoiceDetail_Invoice_ShouldAcceptInvoiceEntity()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();
        var invoice = new Invoice { Id = 5 };

        // Act
        invoiceDetail.Invoice = invoice;

        // Assert
        Assert.Equal(invoice, invoiceDetail.Invoice);
    }

    [Fact]
    public void InvoiceDetail_Product_ShouldAcceptProductEntity()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();
        var product = new Product { Id = 10, Name = "Sample Product" };

        // Act
        invoiceDetail.Product = product;

        // Assert
        Assert.Equal(product, invoiceDetail.Product);
    }

    [Fact]
    public void InvoiceDetail_ModifiedDate_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var invoiceDetail = new InvoiceDetail();

        // Assert
        Assert.Null(invoiceDetail.ModifiedDate);
    }

    [Fact]
    public void InvoiceDetail_ModifiedBy_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var invoiceDetail = new InvoiceDetail();

        // Assert
        Assert.Null(invoiceDetail.ModifiedBy);
    }
}
