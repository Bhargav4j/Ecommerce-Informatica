using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class InvoiceCreateDtoTests
{
    [Fact]
    public void InvoiceCreateDto_ShouldSetAndGetInvoiceDate()
    {
        // Arrange
        var dto = new InvoiceCreateDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.InvoiceDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.InvoiceDate);
    }

    [Fact]
    public void InvoiceCreateDto_ShouldSetAndGetTotalAmount()
    {
        // Arrange
        var dto = new InvoiceCreateDto();
        var expectedValue = 1500.50m;

        // Act
        dto.TotalAmount = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.TotalAmount);
    }

    [Fact]
    public void InvoiceCreateDto_ShouldSetAndGetPersonId()
    {
        // Arrange
        var dto = new InvoiceCreateDto();
        var expectedValue = 1;

        // Act
        dto.PersonId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.PersonId);
    }

    [Fact]
    public void InvoiceCreateDto_ShouldSetAndGetPaymentMethodId()
    {
        // Arrange
        var dto = new InvoiceCreateDto();
        var expectedValue = 1;

        // Act
        dto.PaymentMethodId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.PaymentMethodId);
    }

    [Fact]
    public void InvoiceCreateDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new InvoiceCreateDto();
        var invoiceDate = DateTime.Now;

        // Act
        dto.InvoiceDate = invoiceDate;
        dto.TotalAmount = 1500.50m;
        dto.PersonId = 5;
        dto.PaymentMethodId = 2;

        // Assert
        Assert.Equal(invoiceDate, dto.InvoiceDate);
        Assert.Equal(1500.50m, dto.TotalAmount);
        Assert.Equal(5, dto.PersonId);
        Assert.Equal(2, dto.PaymentMethodId);
    }
}
