using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.UnitTests.Entities;

public class InvoiceTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties_WithDefaultValues()
    {
        // Act
        var invoice = new Invoice();

        // Assert
        Assert.Equal(0, invoice.Id);
        Assert.InRange(invoice.InvoiceDate, DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
        Assert.Equal(0, invoice.TotalAmount);
        Assert.True(invoice.IsActive);
        Assert.InRange(invoice.CreatedDate, DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5));
        Assert.Null(invoice.ModifiedDate);
        Assert.Equal(string.Empty, invoice.CreatedBy);
        Assert.Null(invoice.ModifiedBy);
        Assert.NotNull(invoice.InvoiceDetails);
        Assert.Empty(invoice.InvoiceDetails);
    }

    [Fact]
    public void Id_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.Id = 100;

        // Assert
        Assert.Equal(100, invoice.Id);
    }

    [Fact]
    public void InvoiceDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoice = new Invoice();
        var date = new DateTime(2024, 1, 1);

        // Act
        invoice.InvoiceDate = date;

        // Assert
        Assert.Equal(date, invoice.InvoiceDate);
    }

    [Fact]
    public void TotalAmount_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.TotalAmount = 1500.50m;

        // Assert
        Assert.Equal(1500.50m, invoice.TotalAmount);
    }

    [Fact]
    public void TotalAmount_ShouldHandleZeroValue()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.TotalAmount = 0;

        // Assert
        Assert.Equal(0, invoice.TotalAmount);
    }

    [Fact]
    public void TotalAmount_ShouldHandleNegativeValue()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.TotalAmount = -100;

        // Assert
        Assert.Equal(-100, invoice.TotalAmount);
    }

    [Fact]
    public void IsActive_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.IsActive = false;

        // Assert
        Assert.False(invoice.IsActive);
    }

    [Fact]
    public void PersonId_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.PersonId = 5;

        // Assert
        Assert.Equal(5, invoice.PersonId);
    }

    [Fact]
    public void PaymentMethodId_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.PaymentMethodId = 10;

        // Assert
        Assert.Equal(10, invoice.PaymentMethodId);
    }

    [Fact]
    public void CreatedDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoice = new Invoice();
        var date = new DateTime(2024, 1, 1);

        // Act
        invoice.CreatedDate = date;

        // Assert
        Assert.Equal(date, invoice.CreatedDate);
    }

    [Fact]
    public void ModifiedDate_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoice = new Invoice();
        var date = new DateTime(2024, 1, 2);

        // Act
        invoice.ModifiedDate = date;

        // Assert
        Assert.Equal(date, invoice.ModifiedDate);
    }

    [Fact]
    public void CreatedBy_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.CreatedBy = "Admin";

        // Assert
        Assert.Equal("Admin", invoice.CreatedBy);
    }

    [Fact]
    public void ModifiedBy_ShouldSetAndGetCorrectly()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.ModifiedBy = "Admin";

        // Assert
        Assert.Equal("Admin", invoice.ModifiedBy);
    }

    [Fact]
    public void InvoiceDetails_ShouldBeInitializedAsEmptyList()
    {
        // Act
        var invoice = new Invoice();

        // Assert
        Assert.NotNull(invoice.InvoiceDetails);
        Assert.Empty(invoice.InvoiceDetails);
        Assert.IsAssignableFrom<ICollection<InvoiceDetail>>(invoice.InvoiceDetails);
    }

    [Fact]
    public void Invoice_ShouldAllowInvoiceDetailsToBeAdded()
    {
        // Arrange
        var invoice = new Invoice { Id = 1 };
        var invoiceDetail = new InvoiceDetail { Id = 1, InvoiceId = 1 };

        // Act
        invoice.InvoiceDetails.Add(invoiceDetail);

        // Assert
        Assert.Single(invoice.InvoiceDetails);
        Assert.Contains(invoiceDetail, invoice.InvoiceDetails);
    }
}
