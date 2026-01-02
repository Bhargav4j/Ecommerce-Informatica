using Xunit;
using EcommerceApp.Domain.Entities;
using System;

namespace EcommerceApp.Domain.Entities.Tests;

public class InvoiceTests
{
    [Fact]
    public void Invoice_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var invoice = new Invoice();

        // Assert
        Assert.NotNull(invoice);
        Assert.Equal(0, invoice.Id);
        Assert.Equal(string.Empty, invoice.CustomerDni);
        Assert.Equal(0, invoice.Total);
        Assert.Equal(0, invoice.PaymentMethodId);
        Assert.True(invoice.IsActive);
        Assert.Equal(string.Empty, invoice.CreatedBy);
        Assert.Null(invoice.ModifiedBy);
        Assert.Null(invoice.ModifiedDate);
        Assert.NotNull(invoice.InvoiceDetails);
        Assert.Empty(invoice.InvoiceDetails);
    }

    [Fact]
    public void Invoice_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var invoice = new Invoice();
        var invoiceDate = DateTime.UtcNow;
        var createdDate = DateTime.UtcNow;
        var modifiedDate = DateTime.UtcNow.AddDays(1);
        var customer = new Person { Dni = "12345678" };
        var paymentMethod = new PaymentMethod { Id = 1, Name = "Credit Card" };

        // Act
        invoice.Id = 1;
        invoice.CustomerDni = "12345678";
        invoice.Customer = customer;
        invoice.InvoiceDate = invoiceDate;
        invoice.Total = 1500.50m;
        invoice.PaymentMethodId = 1;
        invoice.PaymentMethod = paymentMethod;
        invoice.IsActive = false;
        invoice.CreatedDate = createdDate;
        invoice.ModifiedDate = modifiedDate;
        invoice.CreatedBy = "Admin";
        invoice.ModifiedBy = "System";

        // Assert
        Assert.Equal(1, invoice.Id);
        Assert.Equal("12345678", invoice.CustomerDni);
        Assert.Equal(customer, invoice.Customer);
        Assert.Equal(invoiceDate, invoice.InvoiceDate);
        Assert.Equal(1500.50m, invoice.Total);
        Assert.Equal(1, invoice.PaymentMethodId);
        Assert.Equal(paymentMethod, invoice.PaymentMethod);
        Assert.False(invoice.IsActive);
        Assert.Equal(createdDate, invoice.CreatedDate);
        Assert.Equal(modifiedDate, invoice.ModifiedDate);
        Assert.Equal("Admin", invoice.CreatedBy);
        Assert.Equal("System", invoice.ModifiedBy);
    }

    [Fact]
    public void Invoice_InvoiceDetails_ShouldBeInitializedAsEmptyList()
    {
        // Arrange & Act
        var invoice = new Invoice();

        // Assert
        Assert.NotNull(invoice.InvoiceDetails);
        Assert.IsAssignableFrom<ICollection<InvoiceDetail>>(invoice.InvoiceDetails);
        Assert.Empty(invoice.InvoiceDetails);
    }

    [Fact]
    public void Invoice_InvoiceDetails_ShouldAllowAddingInvoiceDetails()
    {
        // Arrange
        var invoice = new Invoice { Id = 1 };
        var invoiceDetail = new InvoiceDetail { InvoiceId = invoice.Id, ProductId = 1 };

        // Act
        invoice.InvoiceDetails.Add(invoiceDetail);

        // Assert
        Assert.Single(invoice.InvoiceDetails);
        Assert.Contains(invoiceDetail, invoice.InvoiceDetails);
    }

    [Fact]
    public void Invoice_IsActive_ShouldDefaultToTrue()
    {
        // Arrange & Act
        var invoice = new Invoice();

        // Assert
        Assert.True(invoice.IsActive);
    }

    [Fact]
    public void Invoice_Total_ShouldAcceptDecimalValue()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.Total = 2500.75m;

        // Assert
        Assert.Equal(2500.75m, invoice.Total);
    }

    [Fact]
    public void Invoice_Total_ShouldAcceptZeroValue()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.Total = 0m;

        // Assert
        Assert.Equal(0m, invoice.Total);
    }

    [Fact]
    public void Invoice_InvoiceDate_ShouldAcceptDateTime()
    {
        // Arrange
        var invoice = new Invoice();
        var testDate = new DateTime(2024, 1, 15);

        // Act
        invoice.InvoiceDate = testDate;

        // Assert
        Assert.Equal(testDate, invoice.InvoiceDate);
    }

    [Fact]
    public void Invoice_Customer_ShouldAcceptPersonEntity()
    {
        // Arrange
        var invoice = new Invoice();
        var customer = new Person { Dni = "87654321", FirstName = "Jane", LastName = "Doe" };

        // Act
        invoice.Customer = customer;

        // Assert
        Assert.Equal(customer, invoice.Customer);
    }

    [Fact]
    public void Invoice_PaymentMethod_ShouldAcceptPaymentMethodEntity()
    {
        // Arrange
        var invoice = new Invoice();
        var paymentMethod = new PaymentMethod { Id = 2, Name = "PayPal" };

        // Act
        invoice.PaymentMethod = paymentMethod;

        // Assert
        Assert.Equal(paymentMethod, invoice.PaymentMethod);
    }

    [Fact]
    public void Invoice_ModifiedDate_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var invoice = new Invoice();

        // Assert
        Assert.Null(invoice.ModifiedDate);
    }

    [Fact]
    public void Invoice_ModifiedBy_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var invoice = new Invoice();

        // Assert
        Assert.Null(invoice.ModifiedBy);
    }
}
