using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class InvoiceUpdateDtoTests
{
    [Fact]
    public void InvoiceUpdateDto_ShouldSetAndGetInvoiceDate()
    {
        // Arrange
        var dto = new InvoiceUpdateDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.InvoiceDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.InvoiceDate);
    }

    [Fact]
    public void InvoiceUpdateDto_ShouldSetAndGetTotalAmount()
    {
        // Arrange
        var dto = new InvoiceUpdateDto();
        var expectedValue = 1500.50m;

        // Act
        dto.TotalAmount = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.TotalAmount);
    }

    [Fact]
    public void InvoiceUpdateDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new InvoiceUpdateDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void InvoiceUpdateDto_ShouldSetAndGetPersonId()
    {
        // Arrange
        var dto = new InvoiceUpdateDto();
        var expectedValue = 1;

        // Act
        dto.PersonId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.PersonId);
    }

    [Fact]
    public void InvoiceUpdateDto_ShouldSetAndGetPaymentMethodId()
    {
        // Arrange
        var dto = new InvoiceUpdateDto();
        var expectedValue = 1;

        // Act
        dto.PaymentMethodId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.PaymentMethodId);
    }

    [Fact]
    public void InvoiceUpdateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new InvoiceUpdateDto();
        var invoiceDate = DateTime.Now;

        // Act
        dto.InvoiceDate = invoiceDate;
        dto.TotalAmount = 1500.50m;
        dto.IsActive = true;
        dto.PersonId = 5;
        dto.PaymentMethodId = 2;

        // Assert
        Assert.Equal(invoiceDate, dto.InvoiceDate);
        Assert.Equal(1500.50m, dto.TotalAmount);
        Assert.True(dto.IsActive);
        Assert.Equal(5, dto.PersonId);
        Assert.Equal(2, dto.PaymentMethodId);
    }
}
