using Xunit;
using EcommerceInformatica.Domain.Entities;
using System;
using System.Collections.Generic;

namespace EcommerceInformatica.Domain.Tests;

public class PaymentMethodTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var paymentMethod = new PaymentMethod();

        // Assert
        Assert.NotNull(paymentMethod);
        Assert.Equal(0, paymentMethod.Id);
        Assert.Equal(string.Empty, paymentMethod.Name);
        Assert.Equal(string.Empty, paymentMethod.Description);
        Assert.False(paymentMethod.IsActive);
        Assert.Equal(string.Empty, paymentMethod.CreatedBy);
        Assert.Null(paymentMethod.ModifiedBy);
        Assert.NotNull(paymentMethod.Invoices);
    }

    [Fact]
    public void SetId_ShouldSetValue()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();

        // Act
        paymentMethod.Id = 10;

        // Assert
        Assert.Equal(10, paymentMethod.Id);
    }

    [Fact]
    public void SetName_ShouldSetValue()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();

        // Act
        paymentMethod.Name = "Credit Card";

        // Assert
        Assert.Equal("Credit Card", paymentMethod.Name);
    }

    [Fact]
    public void SetDescription_ShouldSetValue()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();

        // Act
        paymentMethod.Description = "Payment via credit card";

        // Assert
        Assert.Equal("Payment via credit card", paymentMethod.Description);
    }

    [Fact]
    public void SetIsActive_ShouldSetValue()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();

        // Act
        paymentMethod.IsActive = true;

        // Assert
        Assert.True(paymentMethod.IsActive);
    }

    [Fact]
    public void SetCreatedDate_ShouldSetValue()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();
        var date = new DateTime(2026, 1, 12);

        // Act
        paymentMethod.CreatedDate = date;

        // Assert
        Assert.Equal(date, paymentMethod.CreatedDate);
    }

    [Fact]
    public void SetModifiedDate_ShouldSetValue()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();
        var date = new DateTime(2026, 1, 12);

        // Act
        paymentMethod.ModifiedDate = date;

        // Assert
        Assert.Equal(date, paymentMethod.ModifiedDate);
    }

    [Fact]
    public void SetModifiedDate_WithNull_ShouldSetNull()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();

        // Act
        paymentMethod.ModifiedDate = null;

        // Assert
        Assert.Null(paymentMethod.ModifiedDate);
    }

    [Fact]
    public void SetCreatedBy_ShouldSetValue()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();

        // Act
        paymentMethod.CreatedBy = "admin";

        // Assert
        Assert.Equal("admin", paymentMethod.CreatedBy);
    }

    [Fact]
    public void SetModifiedBy_ShouldSetValue()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();

        // Act
        paymentMethod.ModifiedBy = "user";

        // Assert
        Assert.Equal("user", paymentMethod.ModifiedBy);
    }

    [Fact]
    public void SetModifiedBy_WithNull_ShouldSetNull()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();

        // Act
        paymentMethod.ModifiedBy = null;

        // Assert
        Assert.Null(paymentMethod.ModifiedBy);
    }

    [Fact]
    public void SetInvoices_ShouldSetCollection()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();
        var invoices = new List<Invoice>
        {
            new Invoice { Id = 1, TotalAmount = 100m },
            new Invoice { Id = 2, TotalAmount = 200m }
        };

        // Act
        paymentMethod.Invoices = invoices;

        // Assert
        Assert.NotNull(paymentMethod.Invoices);
        Assert.Equal(2, paymentMethod.Invoices.Count);
    }

    [Fact]
    public void Invoices_ShouldBeEmptyListByDefault()
    {
        // Arrange & Act
        var paymentMethod = new PaymentMethod();

        // Assert
        Assert.NotNull(paymentMethod.Invoices);
        Assert.Empty(paymentMethod.Invoices);
    }
}
