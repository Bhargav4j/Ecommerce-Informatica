using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class InvoiceDetailUpdateDtoTests
{
    [Fact]
    public void InvoiceDetailUpdateDto_ShouldSetAndGetQuantity()
    {
        // Arrange
        var dto = new InvoiceDetailUpdateDto();
        var expectedValue = 5;

        // Act
        dto.Quantity = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Quantity);
    }

    [Fact]
    public void InvoiceDetailUpdateDto_ShouldSetAndGetUnitPrice()
    {
        // Arrange
        var dto = new InvoiceDetailUpdateDto();
        var expectedValue = 99.99m;

        // Act
        dto.UnitPrice = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.UnitPrice);
    }

    [Fact]
    public void InvoiceDetailUpdateDto_ShouldSetAndGetSubtotal()
    {
        // Arrange
        var dto = new InvoiceDetailUpdateDto();
        var expectedValue = 499.95m;

        // Act
        dto.Subtotal = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Subtotal);
    }

    [Fact]
    public void InvoiceDetailUpdateDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new InvoiceDetailUpdateDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void InvoiceDetailUpdateDto_ShouldSetAndGetInvoiceId()
    {
        // Arrange
        var dto = new InvoiceDetailUpdateDto();
        var expectedValue = 1;

        // Act
        dto.InvoiceId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.InvoiceId);
    }

    [Fact]
    public void InvoiceDetailUpdateDto_ShouldSetAndGetProductId()
    {
        // Arrange
        var dto = new InvoiceDetailUpdateDto();
        var expectedValue = 1;

        // Act
        dto.ProductId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ProductId);
    }

    [Fact]
    public void InvoiceDetailUpdateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new InvoiceDetailUpdateDto();

        // Act
        dto.Quantity = 5;
        dto.UnitPrice = 99.99m;
        dto.Subtotal = 499.95m;
        dto.IsActive = true;
        dto.InvoiceId = 10;
        dto.ProductId = 20;

        // Assert
        Assert.Equal(5, dto.Quantity);
        Assert.Equal(99.99m, dto.UnitPrice);
        Assert.Equal(499.95m, dto.Subtotal);
        Assert.True(dto.IsActive);
        Assert.Equal(10, dto.InvoiceId);
        Assert.Equal(20, dto.ProductId);
    }
}
