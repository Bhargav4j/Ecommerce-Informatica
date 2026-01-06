using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.UnitTests.Entities;

public class InvoiceDetailTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WithDefaultValues()
    {
        // Act
        var invoiceDetail = new InvoiceDetail();

        // Assert
        Assert.Equal(0, invoiceDetail.Id);
        Assert.Equal(0, invoiceDetail.Quantity);
        Assert.Equal(0, invoiceDetail.UnitPrice);
        Assert.Equal(0, invoiceDetail.Subtotal);
        Assert.True(invoiceDetail.IsActive);
        Assert.InRange(invoiceDetail.CreatedDate, DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
        Assert.Null(invoiceDetail.ModifiedDate);
        Assert.Equal(string.Empty, invoiceDetail.CreatedBy);
        Assert.Null(invoiceDetail.ModifiedBy);
    }

    [Fact]
    public void Id_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.Id = 100;

        // Assert
        Assert.Equal(100, invoiceDetail.Id);
    }

    [Fact]
    public void Quantity_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.Quantity = 5;

        // Assert
        Assert.Equal(5, invoiceDetail.Quantity);
    }

    [Fact]
    public void UnitPrice_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.UnitPrice = 1500.50m;

        // Assert
        Assert.Equal(1500.50m, invoiceDetail.UnitPrice);
    }

    [Fact]
    public void Subtotal_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.Subtotal = 7502.50m;

        // Assert
        Assert.Equal(7502.50m, invoiceDetail.Subtotal);
    }

    [Fact]
    public void Quantity_ShouldHandleZeroValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.Quantity = 0;

        // Assert
        Assert.Equal(0, invoiceDetail.Quantity);
    }

    [Fact]
    public void Quantity_ShouldHandleNegativeValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.Quantity = -5;

        // Assert
        Assert.Equal(-5, invoiceDetail.Quantity);
    }

    [Fact]
    public void UnitPrice_ShouldHandleZeroValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.UnitPrice = 0;

        // Assert
        Assert.Equal(0, invoiceDetail.UnitPrice);
    }

    [Fact]
    public void Subtotal_ShouldHandleZeroValue()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.Subtotal = 0;

        // Assert
        Assert.Equal(0, invoiceDetail.Subtotal);
    }

    [Fact]
    public void IsActive_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.IsActive = false;

        // Assert
        Assert.False(invoiceDetail.IsActive);
    }

    [Fact]
    public void InvoiceId_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.InvoiceId = 5;

        // Assert
        Assert.Equal(5, invoiceDetail.InvoiceId);
    }

    [Fact]
    public void ProductId_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.ProductId = 10;

        // Assert
        Assert.Equal(10, invoiceDetail.ProductId);
    }

    [Fact]
    public void CreatedDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();
        var date = new DateTime(2024, 1, 1);

        // Act
        invoiceDetail.CreatedDate = date;

        // Assert
        Assert.Equal(date, invoiceDetail.CreatedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();
        var date = new DateTime(2024, 1, 2);

        // Act
        invoiceDetail.ModifiedDate = date;

        // Assert
        Assert.Equal(date, invoiceDetail.ModifiedDate);
    }

    [Fact]
    public void CreatedBy_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.CreatedBy = "Admin";

        // Assert
        Assert.Equal("Admin", invoiceDetail.CreatedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoiceDetail = new InvoiceDetail();

        // Act
        invoiceDetail.ModifiedBy = "Admin";

        // Assert
        Assert.Equal("Admin", invoiceDetail.ModifiedBy);
    }
}
