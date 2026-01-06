using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Entities.Tests;

/// <summary>
/// Test class for PaymentMethod entity
/// </summary>
public class PaymentMethodTests
{
    [Fact]
    public void PaymentMethod_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var paymentMethod = new PaymentMethod
        {
            Id = 1,
            Name = "Credit Card",
            Description = "Visa/Mastercard",
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.Equal(1, paymentMethod.Id);
        Assert.Equal("Credit Card", paymentMethod.Name);
        Assert.Equal("Visa/Mastercard", paymentMethod.Description);
        Assert.True(paymentMethod.IsActive);
        Assert.NotNull(paymentMethod.CreatedBy);
    }

    [Fact]
    public void PaymentMethod_Name_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var paymentMethod = new PaymentMethod
            {
                Name = null!,
                CreatedBy = "TestUser"
            };
        });
    }

    [Fact]
    public void PaymentMethod_Orders_ShouldInitializeAsEmptyList()
    {
        // Arrange & Act
        var paymentMethod = new PaymentMethod
        {
            Name = "Credit Card",
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.NotNull(paymentMethod.Orders);
        Assert.Empty(paymentMethod.Orders);
    }

    [Fact]
    public void PaymentMethod_Description_ShouldBeNullable()
    {
        // Arrange & Act
        var paymentMethod = new PaymentMethod
        {
            Name = "Credit Card",
            CreatedBy = "TestUser",
            Description = null
        };

        // Assert
        Assert.Null(paymentMethod.Description);
    }

    [Fact]
    public void PaymentMethod_ModifiedDate_ShouldBeNullable()
    {
        // Arrange & Act
        var paymentMethod = new PaymentMethod
        {
            Name = "Credit Card",
            CreatedBy = "TestUser",
            ModifiedDate = null
        };

        // Assert
        Assert.Null(paymentMethod.ModifiedDate);
    }

    [Fact]
    public void PaymentMethod_IsActive_ShouldDefaultToFalse()
    {
        // Arrange & Act
        var paymentMethod = new PaymentMethod
        {
            Name = "Credit Card",
            CreatedBy = "TestUser"
        };

        // Assert
        Assert.False(paymentMethod.IsActive);
    }

    [Fact]
    public void PaymentMethod_CreatedBy_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var paymentMethod = new PaymentMethod
            {
                Name = "Credit Card",
                CreatedBy = null!
            };
        });
    }

    [Fact]
    public void PaymentMethod_ModifiedBy_ShouldBeNullable()
    {
        // Arrange & Act
        var paymentMethod = new PaymentMethod
        {
            Name = "Credit Card",
            CreatedBy = "TestUser",
            ModifiedBy = null
        };

        // Assert
        Assert.Null(paymentMethod.ModifiedBy);
    }

    [Fact]
    public void PaymentMethod_WithOrders_ShouldMaintainRelationship()
    {
        // Arrange
        var paymentMethod = new PaymentMethod
        {
            Name = "Credit Card",
            CreatedBy = "TestUser"
        };

        var order = new Order
        {
            Status = "Pending",
            CreatedBy = "System",
            PaymentMethodId = paymentMethod.Id
        };

        // Act
        paymentMethod.Orders.Add(order);

        // Assert
        Assert.Single(paymentMethod.Orders);
        Assert.Equal(order, paymentMethod.Orders.First());
    }
}
