using Xunit;
using EcommerceApp.Domain.Entities;
using System;

namespace EcommerceApp.Domain.Entities.Tests;

public class PaymentMethodTests
{
    [Fact]
    public void PaymentMethod_Constructor_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var paymentMethod = new PaymentMethod();

        // Assert
        Assert.NotNull(paymentMethod);
        Assert.Equal(0, paymentMethod.Id);
        Assert.Equal(string.Empty, paymentMethod.Name);
        Assert.Null(paymentMethod.Description);
        Assert.True(paymentMethod.IsActive);
        Assert.Equal(string.Empty, paymentMethod.CreatedBy);
        Assert.Null(paymentMethod.ModifiedBy);
        Assert.Null(paymentMethod.ModifiedDate);
        Assert.NotNull(paymentMethod.Invoices);
        Assert.Empty(paymentMethod.Invoices);
    }

    [Fact]
    public void PaymentMethod_SetProperties_ShouldUpdateValues()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();
        var createdDate = DateTime.UtcNow;
        var modifiedDate = DateTime.UtcNow.AddDays(1);

        // Act
        paymentMethod.Id = 1;
        paymentMethod.Name = "Credit Card";
        paymentMethod.Description = "Visa/Mastercard";
        paymentMethod.IsActive = false;
        paymentMethod.CreatedDate = createdDate;
        paymentMethod.ModifiedDate = modifiedDate;
        paymentMethod.CreatedBy = "Admin";
        paymentMethod.ModifiedBy = "System";

        // Assert
        Assert.Equal(1, paymentMethod.Id);
        Assert.Equal("Credit Card", paymentMethod.Name);
        Assert.Equal("Visa/Mastercard", paymentMethod.Description);
        Assert.False(paymentMethod.IsActive);
        Assert.Equal(createdDate, paymentMethod.CreatedDate);
        Assert.Equal(modifiedDate, paymentMethod.ModifiedDate);
        Assert.Equal("Admin", paymentMethod.CreatedBy);
        Assert.Equal("System", paymentMethod.ModifiedBy);
    }

    [Fact]
    public void PaymentMethod_Invoices_ShouldBeInitializedAsEmptyList()
    {
        // Arrange & Act
        var paymentMethod = new PaymentMethod();

        // Assert
        Assert.NotNull(paymentMethod.Invoices);
        Assert.IsAssignableFrom<ICollection<Invoice>>(paymentMethod.Invoices);
        Assert.Empty(paymentMethod.Invoices);
    }

    [Fact]
    public void PaymentMethod_Invoices_ShouldAllowAddingInvoices()
    {
        // Arrange
        var paymentMethod = new PaymentMethod { Id = 1 };
        var invoice = new Invoice { Id = 1, PaymentMethodId = paymentMethod.Id };

        // Act
        paymentMethod.Invoices.Add(invoice);

        // Assert
        Assert.Single(paymentMethod.Invoices);
        Assert.Contains(invoice, paymentMethod.Invoices);
    }

    [Fact]
    public void PaymentMethod_IsActive_ShouldDefaultToTrue()
    {
        // Arrange & Act
        var paymentMethod = new PaymentMethod();

        // Assert
        Assert.True(paymentMethod.IsActive);
    }

    [Fact]
    public void PaymentMethod_Description_ShouldBeNullableAndAcceptNull()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();

        // Act
        paymentMethod.Description = null;

        // Assert
        Assert.Null(paymentMethod.Description);
    }

    [Fact]
    public void PaymentMethod_Name_ShouldAcceptStringValue()
    {
        // Arrange
        var paymentMethod = new PaymentMethod();
        var name = "PayPal";

        // Act
        paymentMethod.Name = name;

        // Assert
        Assert.Equal(name, paymentMethod.Name);
    }

    [Fact]
    public void PaymentMethod_ModifiedDate_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var paymentMethod = new PaymentMethod();

        // Assert
        Assert.Null(paymentMethod.ModifiedDate);
    }

    [Fact]
    public void PaymentMethod_ModifiedBy_ShouldBeNullByDefault()
    {
        // Arrange & Act
        var paymentMethod = new PaymentMethod();

        // Assert
        Assert.Null(paymentMethod.ModifiedBy);
    }
}
