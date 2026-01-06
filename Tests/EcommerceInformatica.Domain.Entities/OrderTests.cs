using Xunit;
using EcommerceInformatica.Domain.Entities;

namespace EcommerceInformatica.Domain.Entities.Tests;

/// <summary>
/// Test class for Order entity
/// </summary>
public class OrderTests
{
    [Fact]
    public void Order_Constructor_ShouldInitializeProperties()
    {
        // Arrange & Act
        var order = new Order
        {
            Id = 1,
            PersonId = 1,
            OrderDate = DateTime.UtcNow,
            PaymentMethodId = 1,
            TotalAmount = 999.99m,
            Status = "Pending",
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "System"
        };

        // Assert
        Assert.Equal(1, order.Id);
        Assert.Equal(1, order.PersonId);
        Assert.Equal(1, order.PaymentMethodId);
        Assert.Equal(999.99m, order.TotalAmount);
        Assert.Equal("Pending", order.Status);
        Assert.True(order.IsActive);
    }

    [Fact]
    public void Order_Status_ShouldBeRequired()
    {
        // Arrange & Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            var order = new Order
            {
                Status = null!,
                CreatedBy = "System"
            };
        });
    }

    [Fact]
    public void Order_OrderDetails_ShouldInitializeAsEmptyList()
    {
        // Arrange & Act
        var order = new Order
        {
            Status = "Pending",
            CreatedBy = "System"
        };

        // Assert
        Assert.NotNull(order.OrderDetails);
        Assert.Empty(order.OrderDetails);
    }

    [Fact]
    public void Order_NavigationProperties_ShouldBeNullable()
    {
        // Arrange & Act
        var order = new Order
        {
            Status = "Pending",
            CreatedBy = "System"
        };

        // Assert
        Assert.Null(order.Person);
        Assert.Null(order.PaymentMethod);
    }

    [Fact]
    public void Order_ModifiedDate_ShouldBeNullable()
    {
        // Arrange & Act
        var order = new Order
        {
            Status = "Pending",
            CreatedBy = "System",
            ModifiedDate = null
        };

        // Assert
        Assert.Null(order.ModifiedDate);
    }

    [Fact]
    public void Order_IsActive_ShouldDefaultToFalse()
    {
        // Arrange & Act
        var order = new Order
        {
            Status = "Pending",
            CreatedBy = "System"
        };

        // Assert
        Assert.False(order.IsActive);
    }

    [Fact]
    public void Order_TotalAmount_ShouldAcceptDecimalValues()
    {
        // Arrange & Act
        var order = new Order
        {
            Status = "Pending",
            CreatedBy = "System",
            TotalAmount = 12345.67m
        };

        // Assert
        Assert.Equal(12345.67m, order.TotalAmount);
    }

    [Fact]
    public void Order_TotalAmount_ShouldAcceptZeroValue()
    {
        // Arrange & Act
        var order = new Order
        {
            Status = "Pending",
            CreatedBy = "System",
            TotalAmount = 0m
        };

        // Assert
        Assert.Equal(0m, order.TotalAmount);
    }

    [Fact]
    public void Order_WithOrderDetails_ShouldMaintainRelationship()
    {
        // Arrange
        var order = new Order
        {
            Status = "Pending",
            CreatedBy = "System"
        };

        var orderDetail = new OrderDetail
        {
            OrderId = order.Id,
            ProductId = 1,
            Quantity = 2,
            UnitPrice = 99.99m,
            Subtotal = 199.98m,
            CreatedBy = "System"
        };

        // Act
        order.OrderDetails.Add(orderDetail);

        // Assert
        Assert.Single(order.OrderDetails);
        Assert.Equal(orderDetail, order.OrderDetails.First());
    }
}
