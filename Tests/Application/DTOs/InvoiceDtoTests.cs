using EcommerceInformatica.Application.DTOs;
using Xunit;

namespace EcommerceInformatica.Tests.Application.DTOs;

public class InvoiceDtoTests
{
    [Fact]
    public void InvoiceDto_ShouldSetAndGetId()
    {
        // Arrange
        var dto = new InvoiceDto();
        var expectedValue = 1;

        // Act
        dto.Id = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.Id);
    }

    [Fact]
    public void InvoiceDto_ShouldSetAndGetInvoiceDate()
    {
        // Arrange
        var dto = new InvoiceDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.InvoiceDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.InvoiceDate);
    }

    [Fact]
    public void InvoiceDto_ShouldSetAndGetTotalAmount()
    {
        // Arrange
        var dto = new InvoiceDto();
        var expectedValue = 1500.50m;

        // Act
        dto.TotalAmount = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.TotalAmount);
    }

    [Fact]
    public void InvoiceDto_ShouldSetAndGetIsActive()
    {
        // Arrange
        var dto = new InvoiceDto();
        var expectedValue = true;

        // Act
        dto.IsActive = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.IsActive);
    }

    [Fact]
    public void InvoiceDto_ShouldSetAndGetCreatedDate()
    {
        // Arrange
        var dto = new InvoiceDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.CreatedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedDate);
    }

    [Fact]
    public void InvoiceDto_ShouldSetAndGetModifiedDate()
    {
        // Arrange
        var dto = new InvoiceDto();
        var expectedValue = DateTime.Now;

        // Act
        dto.ModifiedDate = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedDate);
    }

    [Fact]
    public void InvoiceDto_ModifiedDate_ShouldAllowNull()
    {
        // Arrange
        var dto = new InvoiceDto();

        // Act
        dto.ModifiedDate = null;

        // Assert
        Assert.Null(dto.ModifiedDate);
    }

    [Fact]
    public void InvoiceDto_ShouldSetAndGetCreatedBy()
    {
        // Arrange
        var dto = new InvoiceDto();
        var expectedValue = "admin";

        // Act
        dto.CreatedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.CreatedBy);
    }

    [Fact]
    public void InvoiceDto_ShouldInitializeCreatedByWithEmptyString()
    {
        // Arrange & Act
        var dto = new InvoiceDto();

        // Assert
        Assert.Equal(string.Empty, dto.CreatedBy);
    }

    [Fact]
    public void InvoiceDto_ShouldSetAndGetModifiedBy()
    {
        // Arrange
        var dto = new InvoiceDto();
        var expectedValue = "admin";

        // Act
        dto.ModifiedBy = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.ModifiedBy);
    }

    [Fact]
    public void InvoiceDto_ModifiedBy_ShouldAllowNull()
    {
        // Arrange
        var dto = new InvoiceDto();

        // Act
        dto.ModifiedBy = null;

        // Assert
        Assert.Null(dto.ModifiedBy);
    }

    [Fact]
    public void InvoiceDto_ShouldSetAndGetPersonId()
    {
        // Arrange
        var dto = new InvoiceDto();
        var expectedValue = 1;

        // Act
        dto.PersonId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.PersonId);
    }

    [Fact]
    public void InvoiceDto_ShouldSetAndGetPersonName()
    {
        // Arrange
        var dto = new InvoiceDto();
        var expectedValue = "John Doe";

        // Act
        dto.PersonName = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.PersonName);
    }

    [Fact]
    public void InvoiceDto_PersonName_ShouldAllowNull()
    {
        // Arrange
        var dto = new InvoiceDto();

        // Act
        dto.PersonName = null;

        // Assert
        Assert.Null(dto.PersonName);
    }

    [Fact]
    public void InvoiceDto_ShouldSetAndGetPaymentMethodId()
    {
        // Arrange
        var dto = new InvoiceDto();
        var expectedValue = 1;

        // Act
        dto.PaymentMethodId = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.PaymentMethodId);
    }

    [Fact]
    public void InvoiceDto_ShouldSetAndGetPaymentMethodName()
    {
        // Arrange
        var dto = new InvoiceDto();
        var expectedValue = "Credit Card";

        // Act
        dto.PaymentMethodName = expectedValue;

        // Assert
        Assert.Equal(expectedValue, dto.PaymentMethodName);
    }

    [Fact]
    public void InvoiceDto_PaymentMethodName_ShouldAllowNull()
    {
        // Arrange
        var dto = new InvoiceDto();

        // Act
        dto.PaymentMethodName = null;

        // Assert
        Assert.Null(dto.PaymentMethodName);
    }

    [Fact]
    public void InvoiceDto_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        var dto = new InvoiceDto();
        var now = DateTime.Now;
        var invoiceDate = DateTime.Now.AddDays(-1);

        // Act
        dto.Id = 1;
        dto.InvoiceDate = invoiceDate;
        dto.TotalAmount = 1500.50m;
        dto.IsActive = true;
        dto.CreatedDate = now;
        dto.ModifiedDate = now.AddDays(1);
        dto.CreatedBy = "admin";
        dto.ModifiedBy = "admin2";
        dto.PersonId = 5;
        dto.PersonName = "John Doe";
        dto.PaymentMethodId = 2;
        dto.PaymentMethodName = "Credit Card";

        // Assert
        Assert.Equal(1, dto.Id);
        Assert.Equal(invoiceDate, dto.InvoiceDate);
        Assert.Equal(1500.50m, dto.TotalAmount);
        Assert.True(dto.IsActive);
        Assert.Equal(now, dto.CreatedDate);
        Assert.Equal(now.AddDays(1), dto.ModifiedDate);
        Assert.Equal("admin", dto.CreatedBy);
        Assert.Equal("admin2", dto.ModifiedBy);
        Assert.Equal(5, dto.PersonId);
        Assert.Equal("John Doe", dto.PersonName);
        Assert.Equal(2, dto.PaymentMethodId);
        Assert.Equal("Credit Card", dto.PaymentMethodName);
    }
}
