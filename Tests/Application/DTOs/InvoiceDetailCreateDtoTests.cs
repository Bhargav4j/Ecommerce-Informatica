using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class InvoiceDetailCreateDtoTests
{
    [Fact]
    public void InvoiceDetailCreateDto_ShouldSetAndGetQuantity()
    {
        // Arrange
        var dto = new InvoiceDetailCreateDto();
        var expectedValue = 5;

        // Act
        dto.Quantity = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Quantity);
    }

    [Fact]
    public void InvoiceDetailCreateDto_ShouldSetAndGetUnitPrice()
    {
        // Arrange
        var dto = new InvoiceDetailCreateDto();
        var expectedValue = 99.99m;

        // Act
        dto.UnitPrice = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.UnitPrice);
    }

    [Fact]
    public void InvoiceDetailCreateDto_ShouldSetAndGetSubtotal()
    {
        // Arrange
        var dto = new InvoiceDetailCreateDto();
        var expectedValue = 499.95m;

        // Act
        dto.Subtotal = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Subtotal);
    }

    [Fact]
    public void InvoiceDetailCreateDto_ShouldSetAndGetInvoiceId()
    {
        // Arrange
        var dto = new InvoiceDetailCreateDto();
        var expectedValue = 1;

        // Act
        dto.InvoiceId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.InvoiceId);
    }

    [Fact]
    public void InvoiceDetailCreateDto_ShouldSetAndGetProductId()
    {
        // Arrange
        var dto = new InvoiceDetailCreateDto();
        var expectedValue = 1;

        // Act
        dto.ProductId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ProductId);
    }

    [Fact]
    public void InvoiceDetailCreateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new InvoiceDetailCreateDto();

        // Act
        dto.Quantity = 5;
        dto.UnitPrice = 99.99m;
        dto.Subtotal = 499.95m;
        dto.InvoiceId = 10;
        dto.ProductId = 20;

        // Assert
        Assert.Equal(5, dto.Quantity);
        Assert.Equal(99.99m, dto.UnitPrice);
        Assert.Equal(499.95m, dto.Subtotal);
        Assert.Equal(10, dto.InvoiceId);
        Assert.Equal(20, dto.ProductId);
    }
}
