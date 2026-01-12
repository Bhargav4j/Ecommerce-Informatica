using Xunit;
using EcommerceInformatica.Domain.Entities;
using System;
using System.Collections.Generic;

namespace EcommerceInformatica.Domain.Tests;

public class InvoiceTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var invoice = new Invoice();

        // Assert
        Assert.NotNull(invoice);
        Assert.Equal(0, invoice.Id);
        Assert.Equal(default(DateTime), invoice.InvoiceDate);
        Assert.Equal(0, invoice.TotalAmount);
        Assert.False(invoice.IsActive);
        Assert.Equal(string.Empty, invoice.CreatedBy);
        Assert.Null(invoice.ModifiedBy);
        Assert.NotNull(invoice.InvoiceDetails);
    }

    [Fact]
    public void SetId_ShouldSetValue()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.Id = 300;

        // Assert
        Assert.Equal(300, invoice.Id);
    }

    [Fact]
    public void SetInvoiceDate_ShouldSetValue()
    {
        // Arrange
        var invoice = new Invoice();
        var date = new DateTime(2026, 1, 12);

        // Act
        invoice.InvoiceDate = date;

        // Assert
        Assert.Equal(date, invoice.InvoiceDate);
    }

    [Fact]
    public void SetTotalAmount_ShouldSetValue()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.TotalAmount = 1500.50m;

        // Assert
        Assert.Equal(1500.50m, invoice.TotalAmount);
    }

    [Fact]
    public void SetTotalAmount_WithZero_ShouldSetValue()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.TotalAmount = 0m;

        // Assert
        Assert.Equal(0m, invoice.TotalAmount);
    }

    [Fact]
    public void SetIsActive_ShouldSetValue()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.IsActive = true;

        // Assert
        Assert.True(invoice.IsActive);
    }

    [Fact]
    public void SetCreatedDate_ShouldSetValue()
    {
        // Arrange
        var invoice = new Invoice();
        var date = new DateTime(2026, 1, 12);

        // Act
        invoice.CreatedDate = date;

        // Assert
        Assert.Equal(date, invoice.CreatedDate);
    }

    [Fact]
    public void SetModifiedDate_ShouldSetValue()
    {
        // Arrange
        var invoice = new Invoice();
        var date = new DateTime(2026, 1, 12);

        // Act
        invoice.ModifiedDate = date;

        // Assert
        Assert.Equal(date, invoice.ModifiedDate);
    }

    [Fact]
    public void SetModifiedDate_WithNull_ShouldSetNull()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.ModifiedDate = null;

        // Assert
        Assert.Null(invoice.ModifiedDate);
    }

    [Fact]
    public void SetCreatedBy_ShouldSetValue()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.CreatedBy = "admin";

        // Assert
        Assert.Equal("admin", invoice.CreatedBy);
    }

    [Fact]
    public void SetModifiedBy_ShouldSetValue()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.ModifiedBy = "user";

        // Assert
        Assert.Equal("user", invoice.ModifiedBy);
    }

    [Fact]
    public void SetModifiedBy_WithNull_ShouldSetNull()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.ModifiedBy = null;

        // Assert
        Assert.Null(invoice.ModifiedBy);
    }

    [Fact]
    public void SetPersonId_ShouldSetValue()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.PersonId = 15;

        // Assert
        Assert.Equal(15, invoice.PersonId);
    }

    [Fact]
    public void SetPerson_ShouldSetValue()
    {
        // Arrange
        var invoice = new Invoice();
        var person = new Person { Id = 5, FirstName = "John", LastName = "Doe" };

        // Act
        invoice.Person = person;

        // Assert
        Assert.NotNull(invoice.Person);
        Assert.Equal(5, invoice.Person.Id);
        Assert.Equal("John", invoice.Person.FirstName);
    }

    [Fact]
    public void SetPaymentMethodId_ShouldSetValue()
    {
        // Arrange
        var invoice = new Invoice();

        // Act
        invoice.PaymentMethodId = 25;

        // Assert
        Assert.Equal(25, invoice.PaymentMethodId);
    }

    [Fact]
    public void SetPaymentMethod_ShouldSetValue()
    {
        // Arrange
        var invoice = new Invoice();
        var paymentMethod = new PaymentMethod { Id = 10, Name = "Credit Card" };

        // Act
        invoice.PaymentMethod = paymentMethod;

        // Assert
        Assert.NotNull(invoice.PaymentMethod);
        Assert.Equal(10, invoice.PaymentMethod.Id);
        Assert.Equal("Credit Card", invoice.PaymentMethod.Name);
    }

    [Fact]
    public void SetInvoiceDetails_ShouldSetCollection()
    {
        // Arrange
        var invoice = new Invoice();
        var invoiceDetails = new List<InvoiceDetail>
        {
            new InvoiceDetail { Id = 1, Quantity = 5 },
            new InvoiceDetail { Id = 2, Quantity = 3 }
        };

        // Act
        invoice.InvoiceDetails = invoiceDetails;

        // Assert
        Assert.NotNull(invoice.InvoiceDetails);
        Assert.Equal(2, invoice.InvoiceDetails.Count);
    }

    [Fact]
    public void InvoiceDetails_ShouldBeEmptyListByDefault()
    {
        // Arrange & Act
        var invoice = new Invoice();

        // Assert
        Assert.NotNull(invoice.InvoiceDetails);
        Assert.Empty(invoice.InvoiceDetails);
    }
}
